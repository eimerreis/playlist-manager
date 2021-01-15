using Application.Common.Interfaces;
using Domain.Common;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests
{
    public class DbContextHelpers
    {
        public static DbContextOptions<DatabaseContext> GetDbContextOptions()
        {
            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(dbName).Options;
            return options;
        }

        public static DatabaseContext CreateDatabaseContext()
        {
            var userService = new Mock<ICurrentUserService>();
            userService.SetupGet(x => x.UserId).Returns("eimerreis");

            var domainEventService = new Mock<IDomainEventService>();
            domainEventService.Setup(x => x.Publish(It.IsAny<DomainEvent>())).Returns(It.IsAny<Task>());

            var options = GetDbContextOptions();
            return new DatabaseContext(options, userService.Object);
        }
    }
}
