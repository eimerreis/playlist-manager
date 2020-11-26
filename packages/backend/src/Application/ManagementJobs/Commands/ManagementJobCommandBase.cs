using System;

namespace Application.ManagementJobs.Commands
{
    public abstract class ManagementJobCommandBase
    {
        public Guid ManagementJobId { get; set; }
    }
}
