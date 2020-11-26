using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ManagementJobs.Commands.StartManagementJob
{
    public class StartManagementJobCommandHandler : IRequestHandler<StartManagementJobCommand>
    {
        private readonly IDatabaseContext _DatabaseContext;

        public StartManagementJobCommandHandler(IDatabaseContext databaseContext)
        {
            _DatabaseContext = databaseContext;
        }

        public async Task<Unit> Handle(StartManagementJobCommand request, CancellationToken cancellationToken)
        {
            var job = await _DatabaseContext.ManagementJobs.FindAsync(request.ManagementJobId);
            if(job == null)
            {
                throw new ManagementJobNotFoundException(request.ManagementJobId);
            }

            job.IsActive = true;
            await _DatabaseContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
