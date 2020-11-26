using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ManagementJobs.Commands.CreateManagementJob
{
    public class CreateManagementJobCommandHandler : IRequestHandler<CreateManagementJobCommand, Guid>
    {
        private readonly IDatabaseContext _DatabaseContext;

        public CreateManagementJobCommandHandler(IDatabaseContext databaseContext)
        {
            _DatabaseContext = databaseContext;
        }

        public async Task<Guid> Handle(CreateManagementJobCommand request, CancellationToken cancellationToken)
        {
            var entityEntry = _DatabaseContext.ManagementJobs.Add(new ManagementJob()
            {
                ArchiveList = request.ArchiveListId,
                Direction = request.Direction,
                MaximumTracks = request.MaximumTracks,
                User = new User
                {
                    Id = request.UserId,
                },
                Playlist = new Playlist
                {
                    Id = request.PlaylistId,
                }
            });
            await _DatabaseContext.SaveChangesAsync(cancellationToken);
            return entityEntry.Entity.Id;
        }
    }
}
