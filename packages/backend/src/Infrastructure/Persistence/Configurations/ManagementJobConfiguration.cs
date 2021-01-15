using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    public class ManagementJobConfiguration : IEntityTypeConfiguration<ManagementJob>
    {
        public void Configure(EntityTypeBuilder<ManagementJob> builder)
        {
            builder.Ignore(m => m.DomainEvents);
            builder.HasKey("Id");
        }
    }
}
