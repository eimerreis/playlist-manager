using MediatR;

namespace Application.Users.Commands.CreateOrUpdateUser
{
    public class CreateOrUpdateUserCommand : IRequest
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
