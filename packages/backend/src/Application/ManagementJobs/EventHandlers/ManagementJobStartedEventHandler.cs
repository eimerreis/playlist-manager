using Application.Common.Models;
using Application.ManagementJobs.Commands.ExecuteManagementJob;
using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ManagementJobs.EventHandlers
{
    public class ManagementJobStartedEventHandler : INotificationHandler<DomainEventNotification<ManagementJobStartedEvent>>
    {
        private readonly ILogger<ManagementJobStartedEventHandler> _Logger;
        private readonly IMediator _Mediator;

        public ManagementJobStartedEventHandler(ILogger<ManagementJobStartedEventHandler> logger, IMediator mediator)
        {
            _Logger = logger;
            _Mediator = mediator;
        }

        public Task Handle(DomainEventNotification<ManagementJobStartedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _Logger.LogInformation($"Management Job has been started.", new { domainEvent });

            _Mediator.Send(new ExecuteManagementJobCommand()
            {
                ManagementJobId = domainEvent.ManagementJob.Id
            });

            return Task.CompletedTask;
        }
    }
}
