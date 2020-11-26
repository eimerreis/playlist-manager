using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ManagementJobs.Commands.CreateManagementJob
{
    /// <summary>
    /// Payload to create a ManagementJob.
    /// Returns the Id of the created ManagementJob
    /// </summary>
    public class CreateManagementJobCommand : IRequest<Guid>
    {
        public string PlaylistId {get; set; }
        public string UserId { get; set; }
        public int MaximumTracks { get; set; }
        public SortDirection Direction { get; set; }
        public string ArchiveListId {get; set; }
    }
}
