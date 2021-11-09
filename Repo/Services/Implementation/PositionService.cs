using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repo.Models;

namespace Repo.Services.Implementation
{
    internal class PositionService : IPositionService
    {
        private readonly RepoContext repoContsext;

        public PositionService(RepoContext repoContsext)
        {
            this.repoContsext = repoContsext;
        }

        public async Task<IList<Position>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await this.repoContsext.Positions.Include(el => el.Childs).Where(el => el.Childs.Count != 0).ToListAsync();
        }

        public async Task<Position> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await this.repoContsext.Positions.FindAsync(id);
        }

        public async Task<Position> GetDirectionByPositionIdAsync(int positionId, CancellationToken cancellationToken)
        {
            return await this.repoContsext.Positions.Where(el => el.Childs.Any(position => position.Id == positionId)).FirstOrDefaultAsync();            
        }

        public async Task<int> InsertAsync(Position position, CancellationToken cancellationToken)
        {
            var grp = await this.repoContsext.Positions.FindAsync(position.Id);
            if(grp == null)
            {
                await this.repoContsext.Positions.AddAsync(position);
                await this.repoContsext.SaveChangesAsync();
            }

            return position.Id;
        }
    }
}
