using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ManagementJobs.Queries
{
    public class GetManagementJobsByUserQueryHandler : IRequestHandler<GetManagementJobsByUserQuery, ManagementJob[]>
    {
        private readonly IDatabaseContext _DatabaseContext;

        public GetManagementJobsByUserQueryHandler(IDatabaseContext databaseContext)
        {
            _DatabaseContext = databaseContext;
        }

        public Task<ManagementJob[]> Handle(GetManagementJobsByUserQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(
                _DatabaseContext.ManagementJobs
                   .Where(x => x.User.Id == request.UserId)
                   .ToArray());
        }
    }
}
