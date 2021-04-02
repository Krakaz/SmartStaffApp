using System.Threading;
using System.Threading.Tasks;

namespace WorkerService.Workers
{
    interface ILoadStaffDataService
    {
        Task LoadAsync(CancellationToken cancellationToken);
    }
}
