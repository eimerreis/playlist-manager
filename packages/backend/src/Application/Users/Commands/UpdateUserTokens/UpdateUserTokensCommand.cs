using MediatR;

namespace Application.Users.Commands.UpdateUserTokens
{
    public class UpdateUserTokensCommand: IRequest
    {
        public string UserId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
