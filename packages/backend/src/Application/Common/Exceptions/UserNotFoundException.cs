using Domain.Entities;

namespace Application.Common.Exceptions
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(string userId) : base(typeof(User), userId) { }
    }
}
