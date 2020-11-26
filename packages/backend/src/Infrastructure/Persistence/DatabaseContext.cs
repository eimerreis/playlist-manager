using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        private readonly ICurrentUserService _CurrentUserService;
        private readonly IDomainEventService _DomainEventService;

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
           : base(options)
        {
        }

        public DatabaseContext(
            DbContextOptions<DatabaseContext> options, ICurrentUserService currentUserService, IDomainEventService domainEventService)
            : base(options)
        {
            _CurrentUserService = currentUserService;
            _DomainEventService = domainEventService;
        }

        public DbSet<ManagementJob> ManagementJobs { get; set; }
        public DbSet<User> Users { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _CurrentUserService.UserId;
                        entry.Entity.Created = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _CurrentUserService.UserId;
                        entry.Entity.LastModified = DateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            builder.HasDefaultSchema("PlaylistManager");
            base.OnModelCreating(builder);
        }

        private async Task DispatchEvents(CancellationToken cancellationToken)
        {
            var domainEventEntities = ChangeTracker.Entries<IHasDomainEvents>()
                .Select(x => x.Entity.DomainEvents)
                .SelectMany(x => x)
                .ToArray();

            foreach (var domainEvent in domainEventEntities)
            {
                await _DomainEventService.Publish(domainEvent);
            }
        }
    }
}
