using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Queries
{
    public class GetUserByIdQuery : IRequest<User>
    {
        public string UserId { get; set; }
    }

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
    {
        private readonly IDatabaseContext _DatabaseContext;

        public GetUserByIdQueryHandler(IDatabaseContext databaseContext)
        {
            _DatabaseContext = databaseContext;
        }

        public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _DatabaseContext.Users.FindAsync(new [] { request.UserId }, cancellationToken);
            if(user == null)
            {
                throw new UserNotFoundException(request.UserId);
            }

            return user;
        }
    }
}
