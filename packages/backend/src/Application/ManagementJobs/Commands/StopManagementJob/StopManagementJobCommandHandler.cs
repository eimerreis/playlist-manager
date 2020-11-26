using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ManagementJobs.Commands.StopManagementJob
{
    public class StopManagementJobCommandHandler : IRequestHandler<StopManagementJobCommand>
    {
        private readonly IDatabaseContext _DatabaseContext;

        public StopManagementJobCommandHandler(IDatabaseContext databaseContext)
        {
            _DatabaseContext = databaseContext;
        }

        public async Task<Unit> Handle(StopManagementJobCommand request, CancellationToken cancellationToken)
        {
            var job = await _DatabaseContext.ManagementJobs.FindAsync(request.ManagementJobId);
            if(job == null)
            {
                throw new ManagementJobNotFoundException(request.ManagementJobId);
            }

            job.IsActive = false;
            await _DatabaseContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
