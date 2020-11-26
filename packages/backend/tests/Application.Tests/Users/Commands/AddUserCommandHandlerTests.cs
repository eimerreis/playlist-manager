using Application.Users.Commands.AddUser;
using AutoFixture;
using FluentAssertions;
using Infrastructure.Persistence;
using System.Threading;
using Xunit;

namespace Application.Tests.Users.Commands
{
    public class AddUserCommandHandlerTests
    {
        [Fact]
        public async void ShouldAddUser()
        {
            var fixture = new Fixture();
            
            using (DatabaseContext dbContext = DbContextHelpers.CreateDatabaseContext())
            {
                var command = fixture.Create<AddUserCommand>();
                var handler = new AddUserCommandHandler(dbContext);

                await handler.Handle(command, CancellationToken.None);

                var created = await dbContext.Users.FindAsync(command.UserId);
                created.Id.Should().Be(command.UserId);
            }
        }
    }
}
