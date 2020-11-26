using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class ManagementJobStoppedEvent : DomainEvent
    {
        public ManagementJobStoppedEvent(ManagementJob managementJob)
        {
            ManagementJob = managementJob;
        }

        public ManagementJob ManagementJob { get; }
    }
}
