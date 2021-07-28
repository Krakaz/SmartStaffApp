using Microsoft.EntityFrameworkCore;
using Repo.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Repo.Services.Implementation
{
    internal class ApplicantService: IApplicantService
    {
        private readonly RepoContext repoContsext;

        public ApplicantService(RepoContext repoContsext)
        {
            this.repoContsext = repoContsext;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var applicant = await this.repoContsext.Applicants.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if(applicant != null)
            {
                this.repoContsext.Applicants.Remove(applicant);
                await this.repoContsext.SaveChangesAsync();
            }
        }

        public async Task<IList<Applicant>> GetApplicantsAsync(CancellationToken cancellationToken)
        {
            return await this.repoContsext.Applicants.ToListAsync();
        }

        public async Task<Applicant> InsertAsyc(Applicant applicant, CancellationToken cancellationToken)
        {
            await this.repoContsext.Applicants.AddAsync(applicant);
            await this.repoContsext.SaveChangesAsync();
            return applicant;
        }

        public async Task<Applicant> UpdateAsync(Applicant applicant, CancellationToken cancellationToken)
        {
            this.repoContsext.Applicants.Update(applicant);
            await this.repoContsext.SaveChangesAsync();
            return applicant;
        }
    }
}
