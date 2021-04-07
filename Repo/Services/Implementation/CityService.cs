using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repo.Models;

namespace Repo.Services.Implementation
{
    internal class CityService : ICityService
    {
        private readonly RepoContext repoContsext;

        public CityService(RepoContext repoContsext)
        {
            this.repoContsext = repoContsext;
        }
        public Task<City> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            return this.repoContsext.Cities.FirstOrDefaultAsync(el => el.Name == name);
        }

        public Task<List<City>> GetListAsync(CancellationToken cancellationToken)
        {
            return this.repoContsext.Cities.ToListAsync();
        }

        public async Task<City> InsertAsync(City city, CancellationToken cancellationToken)
        {
            await this.repoContsext.Cities.AddAsync(city);
            await this.repoContsext.SaveChangesAsync();
            return city;
        }
    }
}
