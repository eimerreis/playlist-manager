using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Text;

namespace Application.ManagementJobs.Queries
{
    public class GetManagementJobsByUserQuery : IRequest<ManagementJob[]>
    {
        public string UserId { get; set; }
    }
}
