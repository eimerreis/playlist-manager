
namespace Api.Models.Request
{
    public class UpdateUserTokensRequest
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
