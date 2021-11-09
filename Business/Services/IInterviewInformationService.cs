using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IInterviewInformationService
    {
        Task LoadIntervievInformationAsync(int year, CancellationToken cancellationToken);
    }
}
