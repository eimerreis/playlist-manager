using Domain.Entities;
using System;

namespace Application.Common.Exceptions
{
    public class ManagementJobNotFoundException : NotFoundException
    {
        public ManagementJobNotFoundException(Guid jobId) : base(typeof(ManagementJob), jobId.ToString()) { }
    }
}
