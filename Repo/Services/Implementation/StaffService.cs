using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repo.Models;

namespace Repo.Services.Implementation
{
    internal class StaffService : IStaffService
    {
        private readonly RepoContext repoContsext;
        private readonly IGroupService groupService;
        private readonly IPositionService positionService;
        private readonly ICityService cityService;

        public StaffService(RepoContext repoContsext, 
            IGroupService groupService, 
            IPositionService positionService,
            ICityService cityService)
        {
            this.repoContsext = repoContsext;
            this.groupService = groupService;
            this.positionService = positionService;
            this.cityService = cityService;
        }

        public async Task<IList<Staff>> GetActiveAsync(CancellationToken cancellationToken)
        {
            return await this.repoContsext.Staffs.Where(el => el.IsActive == true).ToListAsync();
        }

        public async Task<Staff> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await this.repoContsext.Staffs.Include(el => el.Positions).Where(el => el.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IList<Staff>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await this.repoContsext.Staffs
                .Include(el => el.Positions)
                .Where(el => el.Positions.Count > 0)
                .Include(el => el.City)
                .Include(el => el.Groups)
                .ToListAsync();
        }

        public async Task<int> UpsertAsync(Staff staff, CancellationToken cancellationToken)
        {
            var staffDB = await this.repoContsext.Staffs.Include(el => el.City).FirstOrDefaultAsync(el => el.Id == staff.Id);

            var city = await this.cityService.GetByNameAsync(staff.City.Name, cancellationToken);
            if(city is null)
            {
                city = await this.cityService.InsertAsync(staff.City, cancellationToken);
            }
            staff.City = city;

            if (staffDB == null)
            {
                for(int i = 0; i < staff.Groups.Count; i++)
                {
                    var grp = await this.groupService.GetByIdAsync(staff.Groups[i].Id, cancellationToken);
                    if(grp == null)
                    {
                        grp = staff.Groups[i];
                        await this.groupService.InsertAsync(grp, cancellationToken);
                    }
                    staff.Groups[i] = grp;                    
                }

                for (int i = 0; i < staff.Positions.Count; i++)
                {
                    var pos = await this.positionService.GetByIdAsync(staff.Positions[i].Id, cancellationToken);
                    if (pos == null)
                    {
                        pos = staff.Positions[i];
                        await this.positionService.InsertAsync(pos, cancellationToken);
                    }
                    staff.Positions[i] = pos;
                }                

                await this.repoContsext.Staffs.AddAsync(staff);
                await this.repoContsext.SaveChangesAsync();
            }
            else
            {
                if(staffDB.MiddleName != staff.MiddleName || staffDB.Female is null || staffDB.Birthday is null || staffDB.City is null)
                {
                    staffDB.MiddleName = staff.MiddleName;
                    staffDB.FullName = staff.FullName;
                    staffDB.Birthday = staff.Birthday;
                    staffDB.Female = staff.Female;
                    staffDB.City = city;
                    this.repoContsext.Staffs.Update(staffDB);
                    await this.repoContsext.SaveChangesAsync();
                }
            }

            return staff.Id;
        }

        public async Task<Staff> UpdateAsync(Staff staff, CancellationToken cancellationToken)
        {
            this.repoContsext.Staffs.Update(staff);
            await this.repoContsext.SaveChangesAsync();
            return staff;
        }

        public async Task<IList<Staff>> GetAllByBranchIdAsync(int branchId, CancellationToken cancellationToken)
        {
            return await this.repoContsext.Staffs
                .Include(el => el.Positions)
                .Where(el => el.Positions.Count > 0)
                .Include(el => el.City)
                .Include(el => el.Groups)
                .Where(el => el.Groups.Any(gr => gr.Id == branchId))
                .ToListAsync();
            
        }
    }
}
