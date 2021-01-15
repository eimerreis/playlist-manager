using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Linq;
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
            var existing = _DatabaseContext.ManagementJobs.FirstOrDefault(x => x.PlaylistId == request.PlaylistId && x.UserId == request.UserId);
            if (existing == null)
            {
                var entityEntry = _DatabaseContext.ManagementJobs.Add(new ManagementJob()
                {
                    ArchiveList = request.ArchiveListId,
                    Direction = request.Direction,
                    MaximumTracks = request.MaximumTracks,
                    User = await _DatabaseContext.Users.FindAsync(request.UserId),
                    PlaylistId = request.PlaylistId,
                });
                await _DatabaseContext.SaveChangesAsync(cancellationToken);
                return entityEntry.Entity.Id;
            }
            else
            {
                existing.ArchiveList = request.ArchiveListId;
                existing.Direction = request.Direction;
                existing.MaximumTracks = request.MaximumTracks;

                await _DatabaseContext.SaveChangesAsync(cancellationToken);
                return existing.Id;
            }
        }
    }
}
