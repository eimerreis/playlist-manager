using Microsoft.AspNetCore.Http;

namespace Api.Services.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetAccessToken(this HttpContext context)
        {
            return context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        }
    }
}
