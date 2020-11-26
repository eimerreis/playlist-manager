using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.ManagementJobs.Commands.CreateManagementJob;
using Application.ManagementJobs.Commands.ExecuteManagementJob;
using Application.ManagementJobs.Commands.StartManagementJob;
using AutoFixture;
using MediatR;
using Moq;
using System;
using System.Threading;
using Xunit;

namespace Application.Tests.ManagementJobs.Commands
{
    public class ExecuteManagementJobCommandHandlerTests
    {
        [Fact]
        public async void ShouldThrowIfJobDoesNotExist()
        {
            var fixture = new Fixture();
            var queueClient = new Mock<IQueueClient>();
            var mediator = new Mock<IMediator>();
            var userService = new Mock<ICurrentUserService>();

            // id of management job will not exist in database, therefore execption will be thrown
            var command = fixture.Create<ExecuteManagementJobCommand>();

            using(var dbContext = DbContextHelpers.CreateDatabaseContext())
            {
                var handler = new ExecuteManagementJobCommandHandler(dbContext, queueClient.Object, mediator.Object, userService.Object);
                await Assert.ThrowsAsync<ManagementJobNotFoundException>(() => handler.Handle(command, CancellationToken.None));
            }
        }

        [Fact]
        public async void ShouldNotQueueNextExecutionIfInactive()
        {
            var fixture = new Fixture();
            var queueClient = new Mock<IQueueClient>();
            var mediator = new Mock<IMediator>();
            var userService = new Mock<ICurrentUserService>();

            var createCommand = fixture.Create<CreateManagementJobCommand>();

            using(var dbContext = DbContextHelpers.CreateDatabaseContext())
            {
                var createCommandHandler = new CreateManagementJobCommandHandler(dbContext);
                var createdId = await createCommandHandler.Handle(createCommand, CancellationToken.None);

                var executeCommand = new ExecuteManagementJobCommand
                {
                    ManagementJobId = createdId
                };

                var executeCommandHandler = new ExecuteManagementJobCommandHandler(dbContext, queueClient.Object, mediator.Object, userService.Object);
                await executeCommandHandler.Handle(executeCommand, CancellationToken.None);

                // should not call queue client because job is set to inactive
                queueClient.Verify(x => x.EnqueueManagementJob(executeCommand), Times.Never);
            }
        }

        [Fact]
        public async void ShouldQueueNextExecution()
        {
            var fixture = new Fixture();
            var queueClient = new Mock<IQueueClient>();
            var mediator = new Mock<IMediator>();
            var userService = new Mock<ICurrentUserService>();

            var createCommand = fixture.Create<CreateManagementJobCommand>();

            using(var dbContext = DbContextHelpers.CreateDatabaseContext())
            {
                var createCommandHandler = new CreateManagementJobCommandHandler(dbContext);
                var createdId = await createCommandHandler.Handle(createCommand, CancellationToken.None);

                // set the job to "IsActive"
                var startCommand = new StartManagementJobCommand
                {
                    ManagementJobId = createdId
                };
                var startCommandHandler = new StartManagementJobCommandHandler(dbContext);
                await startCommandHandler.Handle(startCommand, CancellationToken.None);

                var executeCommand = new ExecuteManagementJobCommand
                {
                    ManagementJobId = createdId
                };

                var executeCommandHandler = new ExecuteManagementJobCommandHandler(dbContext, queueClient.Object, mediator.Object, userService.Object);
                await executeCommandHandler.Handle(executeCommand, CancellationToken.None);

                // should not call queue client because job is set to inactive
                queueClient.Verify(x => x.EnqueueManagementJob(executeCommand), Times.Once);
            }
        }
    }
}
