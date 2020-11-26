using Application.Common.Models;
using Application.ManagementJobs.Commands.ExecuteManagementJob;
using Infrastructure.MessageBus;
using System;
using Xunit;

namespace Infrastructure.IntegrationTests
{
    public class ServiceBusQueueClientTests
    {
        [Fact]
        public async void ShouldSendMessage()
        {
            var client = new ServiceBusQueueClient(new QueueClientConfiguration
            {
                ConnectionString = "Endpoint=sb://sbplaylistmanager.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=yD/JLATqZXAaZcPoMWMCtdBf+0Bn/wRx/tFl2OWCZOI=",
                QueueName = "jobs"
            });

            await client.EnqueueManagementJob(new ExecuteManagementJobCommand
            {
                ManagementJobId = Guid.Parse("0785932f-0e76-4ef8-8e68-bb54b2b94872")
            });
        }
    }
}
