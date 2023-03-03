using System.Threading;
using System.Threading.Tasks;
using Application.DTO.ActionLog.Requests;

namespace Application.Logging.ActionLog.Services
{
    public interface IActionLogService
    {
        Task LogSucceededEventAsync(CreateActionLogRequest createActionLogRequest, CancellationToken cancellationToken = default(CancellationToken));

        Task LogFailedEventAsync(CreateActionLogRequest createActionLogRequest, CancellationToken cancellationToken = default(CancellationToken));
    }
}