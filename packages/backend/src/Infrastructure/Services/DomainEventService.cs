using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class DomainEventService : IDomainEventService
    {
        private readonly IPublisher _Mediator;
        private readonly ILogger _Logger;

        public DomainEventService(ILogger<DomainEventService> logger, IPublisher mediator)
        {
            _Logger = logger;
            _Mediator = mediator;
        }

        public async Task Publish(DomainEvent domainEvent)
        {
            _Logger.LogInformation("Publishing domain event. Event - {event}", domainEvent.GetType().Name);
            await _Mediator.Publish(GetNotificationCorrespondingToDomainEvent(domainEvent));
        }

        private INotification GetNotificationCorrespondingToDomainEvent(DomainEvent domainEvent)
        {
            return (INotification)Activator.CreateInstance(
                typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType()), domainEvent);
        }
    }
}
