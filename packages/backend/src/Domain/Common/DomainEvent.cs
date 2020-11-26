using System;
using System.Collections.Generic;

namespace Domain.Common
{
    public interface IHasDomainEvents
    {
        List<DomainEvent> DomainEvents { get; set; }
    }

    public abstract class DomainEvent
    {
        protected DomainEvent()
        {
            DateOccured = DateTime.UtcNow;
            EventId = Guid.NewGuid();
        }

        public DateTimeOffset DateOccured { get; set; } = DateTime.UtcNow;
        public Guid EventId { get; set; }
    }
}
