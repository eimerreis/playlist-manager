using System;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.ManagementJobs.Commands.ExecuteManagementJob;
using MediatR;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace ManagementJobFunction
{
    public class ManagementJobQueueProcessor
    {
        private readonly IMediator _Mediator;

        public ManagementJobQueueProcessor(IMediator mediator)
        {
            _Mediator = mediator;
        }

        [FunctionName("ManagementJobQueueProcessor")]
        public async Task Run([ServiceBusTrigger("jobs", Connection = "ServiceBusConnectionString")]ExecuteManagementJobCommand command, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message");
            await _Mediator.Send(command);
        }
    }
}
