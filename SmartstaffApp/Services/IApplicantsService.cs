using SmartstaffApp.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SmartstaffApp.Services
{
    public interface IApplicantsService
    {
        Task<IList<Applicant>> GetApplicantsListAsync(CancellationToken cancellationToken);
    }
}
