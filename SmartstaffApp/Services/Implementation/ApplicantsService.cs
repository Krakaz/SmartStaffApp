using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SmartstaffApp.Models;

namespace SmartstaffApp.Services.Implementation
{
    internal class ApplicantsService : IApplicantsService
    {
        private readonly DataLoader.Maketalents.Services.IMaketalentsService maketalentsService;
        private readonly Repo.Services.IApplicantService applicantService;
        private readonly Business.Services.IMessageService messageService;

        public ApplicantsService(DataLoader.Maketalents.Services.IMaketalentsService maketalentsService,
            Repo.Services.IApplicantService applicantService,
            Business.Services.IMessageService messageService)
        {
            this.maketalentsService = maketalentsService;
            this.applicantService = applicantService;
            this.messageService = messageService;
        }

        private static DateTime JavaTimeStampToDateTime(double javaTimeStamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(javaTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public async Task<IList<ApplicantVM>> GetApplicantsListAsync(CancellationToken cancellationToken)
        {
            var applicants = await this.applicantService.GetApplicantsAsync(cancellationToken);
            var result = applicants.Select(el => 
                    new ApplicantVM
                    {
                        Id = el.Id,
                        Email = el.Email,
                        EnglishLevel = el.EnglishLevel,
                        FullName = el.FullName,
                        Phone = el.Phone,
                        Position = el.Position,
                        Status = el.Status,
                        LastActivity = el.LastActivity
                    }
            ).ToList();
            return result;
        }

      
        private async Task<string> InsertApplicantAsunc(DataLoader.Maketalents.Models.Entity entity, CancellationToken cancellationToken)
        {
            await this.applicantService.InsertAsyc(
                        new Repo.Models.Applicant
                        {
                            Id = entity.id,
                            Email = entity.email,
                            EnglishLevel = string.Join(", ", entity.englishLevel),
                            FullName = entity.fullName,
                            Phone = string.Join(", ", entity.phone),
                            Position = string.Join(", ", entity.positions),
                            Status = entity.status,
                            LastActivity = JavaTimeStampToDateTime(entity.lastActivity)
                        },
                        cancellationToken
            );
            return $"Появился новый соискатель {entity.fullName} претендующий на должность {string.Join(", ", entity.positions)}. Статус соискателя: {entity.status}{Environment.NewLine}" + 
                $"ссылка на профиль: https://smartstaff.simbirsoft1.com/pages/employee/view.xhtml?id={entity.id}{Environment.NewLine}";
        }

        private async Task<string> UpdateApplicantAsync(Repo.Models.Applicant applicant,DataLoader.Maketalents.Models.Entity entity, CancellationToken cancellationToken)
        {
            var msg = "";
            applicant.Email = entity.email;
            applicant.EnglishLevel = string.Join(", ", entity.englishLevel);
            applicant.FullName = entity.fullName;
            applicant.Phone = string.Join(", ", entity.phone);
            applicant.Position = string.Join(", ", entity.positions);
            applicant.LastActivity = JavaTimeStampToDateTime(entity.lastActivity);

            if (applicant.Status != entity.status)
            {
                msg += $"Статус соискателя {applicant.FullName} претендующего на должность {applicant.Position} изменился с {applicant.Status} на {entity.status}{Environment.NewLine}" +
                $"ссылка на профиль: https://smartstaff.simbirsoft1.com/pages/employee/view.xhtml?id={applicant.Id}{Environment.NewLine}";
                applicant.Status = entity.status;
            }
            await this.applicantService.UpdateAsync(applicant, cancellationToken);
            return msg;
        }

        public async Task LoafApplicantsAsync(CancellationToken cancellationToken)
        {
            var result = new List<ApplicantVM>();
            var mtResponse = await this.maketalentsService.LoadApplicantsAsync(cancellationToken);
            var applicants = await this.applicantService.GetApplicantsAsync(cancellationToken);
            var msg = "";
            foreach (var entity in mtResponse.entities)
            {
                var applicant = applicants.FirstOrDefault(x => x.Id == entity.id);

                if (applicant == null)
                {
                    msg += await this.InsertApplicantAsunc(entity, cancellationToken);
                }
                else
                {
                    msg += await this.UpdateApplicantAsync(applicant, entity, cancellationToken);
                }
            }

            foreach (var applicant in applicants.Where(el => !mtResponse.entities.Any(ap => ap.id == el.Id)))
            {
                msg += $"Cоискатель {applicant.FullName} претендующего на должность {applicant.Position} пропал из списка соискателей{Environment.NewLine}" +
                $"ссылка на профиль: https://smartstaff.simbirsoft1.com/pages/employee/view.xhtml?id={applicant.Id}{Environment.NewLine}";
                await this.applicantService.DeleteAsync(applicant.Id, cancellationToken);
            }

            if (!string.IsNullOrEmpty(msg))
            {
                await this.messageService.SendMessageAsync(Business.Enums.MessageType.Applicants, msg, cancellationToken);
            }
        }
    }
}
