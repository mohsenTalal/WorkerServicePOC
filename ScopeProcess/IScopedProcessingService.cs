using System.Threading;
using System.Threading.Tasks;

namespace WorkerServicePOC.ScopeProcess
{
    public interface IScopedProcessingService
    {
        Task DoWork(CancellationToken stoppingToken);
    }
}