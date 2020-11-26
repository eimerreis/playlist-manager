using MediatR;

namespace Application.Users.Commands.AddUser
{
    public class AddUserCommand: IRequest
    {
        public string UserId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
