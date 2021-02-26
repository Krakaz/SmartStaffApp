using System.Threading;
using System.Threading.Tasks;
using Repo.Models;

namespace Repo.Services.Implementation
{
    internal class GroupService : IGroupService
    {
        private readonly RepoContext repoContsext;

        public GroupService(RepoContext repoContsext)
        {
            this.repoContsext = repoContsext;
        }

        public async Task<Group> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await this.repoContsext.Groups.FindAsync(id);
        }

        public async Task<int> InsertAsync(Group group, CancellationToken cancellationToken)
        {
            var grp = await this.repoContsext.Groups.FindAsync(group.Id);
            if(grp == null)
            {
                await this.repoContsext.Groups.AddAsync(group);
                await this.repoContsext.SaveChangesAsync();
            }

            return group.Id;
        }
    }
}
