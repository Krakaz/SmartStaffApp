using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Enums;

namespace Business.Services.Implementation
{
    internal class StaffService : IStaffService
    {
        private readonly DataLoader.Maketalents.Services.IMaketalentsService maketalentsService;
        private readonly Repo.Services.IStaffService repoStaffService;
        private readonly Repo.Services.IPositionService repoPositionService;
        private readonly GoogleSheetsWorker.Services.INewStaffService googleSheetsNewStaffService;
        private readonly IMessageService messageService;

        public StaffService(DataLoader.Maketalents.Services.IMaketalentsService maketalentsService,
            Repo.Services.IStaffService repoStaffService,
            Repo.Services.IPositionService repoPositionService,
            GoogleSheetsWorker.Services.INewStaffService googleSheetsNewStaffService,
            IMessageService messageService)
        {
            this.maketalentsService = maketalentsService;
            this.repoStaffService = repoStaffService;
            this.repoPositionService = repoPositionService;
            this.googleSheetsNewStaffService = googleSheetsNewStaffService;
            this.messageService = messageService;
        }
        public async Task UpsertNewStaffAsync(CancellationToken cancellationToken)
        {
            var currentStaff = await this.maketalentsService.LoadNewStaffAsync(cancellationToken);

            foreach (var sourceStaff in currentStaff)
            {

                var dtoStaff = new Repo.Models.Staff
                {
                    Id = sourceStaff.id,
                    Birthday = string.IsNullOrEmpty(sourceStaff.birthday) ? (DateTime?)null : DateTime.Parse(sourceStaff.birthday),
                    Email = sourceStaff.email,
                    Female = sourceStaff.female,
                    FirstName = sourceStaff.firstName,
                    LastName = sourceStaff.lastName,
                    MiddleName = sourceStaff.middleName,
                    FullName = sourceStaff.fullName,
                    FirstWorkingDate = DateTime.Parse(sourceStaff.firstWorkingDate),
                    Phones = string.Join(", ", sourceStaff.phones),
                    Skype = sourceStaff.skype,
                    IsArived = false,
                    IsActive = true,
                    Groups = new List<Repo.Models.Group>(),
                    Positions = new List<Repo.Models.Position>()
                };

                foreach (var sourceGroup in sourceStaff.groups)
                {
                    dtoStaff.Groups.Add(
                        new Repo.Models.Group() { Id = sourceGroup.id, Name = sourceGroup.name }
                        );
                }

                foreach (var sourcePosition in sourceStaff.positions)
                {
                    dtoStaff.Positions.Add(
                        new Repo.Models.Position() { Id = sourcePosition.id, Name = sourcePosition.name }
                        );
                }

                dtoStaff.City = new Repo.Models.City { Name = sourceStaff.city };

                var staff = await this.repoStaffService.GetByIdAsync(sourceStaff.id, cancellationToken);
                if (staff is null)
                {
                    await this.messageService.SendMessageAsync(MessageType.NewStaff, $"{dtoStaff.FullName} новый сотрудник. На позиции {string.Join(", ", dtoStaff.Positions.Select(el => el.Name))}", cancellationToken);
                    var direction = await this.repoPositionService.GetDirectionByPositionIdAsync(dtoStaff.Positions.FirstOrDefault().Id, cancellationToken);
                    this.googleSheetsNewStaffService.AddNewStaffToSheetAsync(dtoStaff.FullName, dtoStaff.FirstWorkingDate, direction?.Name);
                }

                await this.repoStaffService.UpsertAsync(dtoStaff, cancellationToken);
            }

            // Теперь понять кто уволен и их тоже учесть.
            var staffs = await this.repoStaffService.GetActiveAsync(cancellationToken);

            var firedStaffs = staffs.Where(el => !currentStaff.Any(staff => staff.id == el.Id));
            foreach(var staff in firedStaffs)
            {
                staff.IsActive = false;                
                staff.NotActiveDate = DateTime.Now.Date; // Нет способа получить дату увольнения, потому ставим текущую
                await this.repoStaffService.UpdateAsync(staff, cancellationToken);
                await this.messageService.SendMessageAsync(MessageType.Fired, $"{staff.FullName} уволен с позиции {staff.Positions.First().Name}.", cancellationToken);
            }

        }
    }
}
