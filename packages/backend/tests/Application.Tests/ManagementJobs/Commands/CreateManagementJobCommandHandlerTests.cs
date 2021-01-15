using Application.ManagementJobs.Commands.CreateManagementJob;
using AutoFixture;
using FluentAssertions;
using Infrastructure.Persistence;
using System.Threading;
using Xunit;

namespace Application.Tests.ManagementJobs.Commands
{
    public class CreateManagementJobCommandHandlerTests
    {
        [Fact]
        public async void CreateManagementJobCommand_InsertsIntoDatabase()
        {
            var fixture = new Fixture();
            
            using (DatabaseContext dbContext = DbContextHelpers.CreateDatabaseContext())
            {
                var command = fixture.Create<CreateManagementJobCommand>();
                var handler = new CreateManagementJobCommandHandler(dbContext);

                var result = await handler.Handle(command, CancellationToken.None);
                result.Should().NotBeEmpty();

                var created = await dbContext.ManagementJobs.FindAsync(result);
                created.PlaylistId.Should().Be(command.PlaylistId);
            }
        }
    }
}
