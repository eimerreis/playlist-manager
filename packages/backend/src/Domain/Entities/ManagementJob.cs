using Domain.Common;
using Domain.Enums;
using Domain.Events;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class ManagementJob : AuditableEntity, IHasDomainEvents
    {
        public ManagementJob()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string PlaylistId { get; set; }

        public int MaximumTracks { get; set; }
        public string ArchiveList { get; set; }
        public SortDirection Direction { get; set; }

        public string UserId {get; set;}
        public User User { get; set; }

        private bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            set {
                if(value == true && _isActive == false)
                {
                    DomainEvents.Add(new ManagementJobStartedEvent(this));
                }
                if(value == false && IsActive == true)
                {
                    DomainEvents.Add(new ManagementJobStoppedEvent(this));
                }
                _isActive = value;
            }
        }

        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}
