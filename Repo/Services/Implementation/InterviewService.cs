using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repo.Models;

namespace Repo.Services.Implementation
{
    internal class InterviewService : IInterviewService
    {
        private RepoContext db;
        public InterviewService(RepoContext repoContsext)
        {
            this.db = repoContsext;
        }

        public async Task<IList<Interview>> GetByYear(int year, CancellationToken cancellationToken)
        {
            return await this.db.Interviews.Where(x => x.Year == year).ToListAsync();
        }

        public async Task UpsertAsync(IList<Interview> interviews, int year, CancellationToken cancellationToken)
        {
            var records = this.db.Interviews.Where(x => x.Year == year);
            this.db.Interviews.RemoveRange(records);
            this.db.Interviews.AddRange(interviews);
            await this.db.SaveChangesAsync();
        }
    }
}
