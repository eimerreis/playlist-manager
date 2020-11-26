using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class ManagementJobStartedEvent : DomainEvent
    {
        public ManagementJobStartedEvent(ManagementJob job)
        {
            ManagementJob = job;
        }

        public ManagementJob ManagementJob { get; }
    }
}
