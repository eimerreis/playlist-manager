using Application.ManagementJobs.Commands.ExecuteManagementJob;
using System;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System.Text;
using Application.Common.Models;

namespace Infrastructure.MessageBus
{
    public class ServiceBusQueueClient : Application.Common.Interfaces.IQueueClient
    {
        private readonly Microsoft.Azure.ServiceBus.IQueueClient _QueueClient;

        public ServiceBusQueueClient(QueueClientConfiguration queueClientConfiguration)
        {
            _QueueClient = CreateQueueClient(queueClientConfiguration);
        }

        public async Task EnqueueManagementJob(ExecuteManagementJobCommand command)
        {
            var message = new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(command)));
            message.ScheduledEnqueueTimeUtc = DateTime.UtcNow.AddSeconds(20);
            message.ContentType = "application/json";

            await _QueueClient.SendAsync(message);
        }

        private QueueClient CreateQueueClient(QueueClientConfiguration queueClientConfiguration)
        {
            return new QueueClient(queueClientConfiguration.ConnectionString, queueClientConfiguration.QueueName);
        }
    }
}
