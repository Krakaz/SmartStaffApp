using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SmartstaffApp.Models;

namespace SmartstaffApp.Services.Implementation
{
    internal class ApplicantsService: IApplicantsService
    {
        private readonly DataLoader.Maketalents.Services.IMaketalentsService maketalentsService;

        public ApplicantsService(DataLoader.Maketalents.Services.IMaketalentsService maketalentsService)
        {
            this.maketalentsService = maketalentsService;
        }

        private static DateTime JavaTimeStampToDateTime(double javaTimeStamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(javaTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public async Task<IList<Applicant>> GetApplicantsListAsync(CancellationToken cancellationToken)
        {
            var result = new List<Applicant>();
            var mtResponse = await this.maketalentsService.LoadApplicantsAsync(cancellationToken);
            foreach(var entity in mtResponse.entities)
            {
                result.Add(
                    new Applicant
                    {
                        Id = entity.id,
                        Email = entity.email,
                        EnglishLevel = string.Join(", ", entity.englishLevel),
                        FullName = entity.fullName,
                        Phone = string.Join(", ", entity.phone),
                        Position = string.Join(", ", entity.positions),
                        Status = entity.status,
                        LastActivity = JavaTimeStampToDateTime(entity.lastActivity)
                    }
                    );
            }


            return result;
        }
    }
}
