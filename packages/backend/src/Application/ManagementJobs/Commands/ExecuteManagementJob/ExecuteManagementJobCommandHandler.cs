using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Playlists.Commands.RemoveAndArchiveTracks;
using Application.Playlists.Commands.SortPlaylist;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ManagementJobs.Commands.ExecuteManagementJob
{
    public class ExecuteManagementJobCommandHandler : IRequestHandler<ExecuteManagementJobCommand>
    {
        private readonly IDatabaseContext _DatabaseContext;
        private readonly IQueueClient _QueueClient;
        private readonly IMediator _Mediator;
        private readonly ICurrentUserService _CurrentUserService;

        public ExecuteManagementJobCommandHandler(IDatabaseContext databaseContext, IQueueClient queueClient, IMediator mediator, ICurrentUserService currentUserService)
        {
            _DatabaseContext = databaseContext;
            _QueueClient = queueClient;
            _Mediator = mediator;
            _CurrentUserService = currentUserService;
        }

        public async Task<Unit> Handle(ExecuteManagementJobCommand request, CancellationToken cancellationToken)
        {
            var job = await _DatabaseContext.ManagementJobs.FindAsync(request.ManagementJobId);
            if(job == null)
            {
                throw new ManagementJobNotFoundException(request.ManagementJobId);
            }

            // manage playlist => place logic here !?
            // maybe better to enqueue other commands for each step => separation of concerns
            await _Mediator.Send(new SortPlaylistCommand { Direction = job.Direction, PlaylistId = job.PlaylistId });
            await _Mediator.Send(new RemoveAndArchiveTracksCommand { PlaylistId = job.PlaylistId, ArchiveListId = job.ArchiveList, MaximumTracks = job.MaximumTracks });

            // see if job is still running in db and queue new execution
            job = await _DatabaseContext.ManagementJobs.FindAsync(request.ManagementJobId);
            if(job.IsActive)
            {
                await _QueueClient.EnqueueManagementJob(request);
            }

            return Unit.Value;
        }
    }
}
