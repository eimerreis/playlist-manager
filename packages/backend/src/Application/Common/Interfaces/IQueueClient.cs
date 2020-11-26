using Application.ManagementJobs.Commands.ExecuteManagementJob;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IQueueClient
    {
        Task EnqueueManagementJob(ExecuteManagementJobCommand command);
    }
}
