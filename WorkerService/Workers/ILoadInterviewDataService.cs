using System.Threading;
using System.Threading.Tasks;

namespace WorkerService.Workers
{
    interface ILoadInterviewDataService
    {
        Task LoadAsync(CancellationToken cancellationToken);
    }
}
