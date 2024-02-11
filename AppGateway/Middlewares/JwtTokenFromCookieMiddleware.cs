using AppGateway.Helpers;

namespace AppGateway.Middlewares
{
    public class JwtTokenFromCookieMiddleware
    {
        private readonly RequestDelegate next;

        public JwtTokenFromCookieMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Cookies[AuthOptions.Cookie.Name];

            if (!string.IsNullOrEmpty(token) && TokenHelper.ValidateToken(token))
            {
                context.Request.Headers.Add("Authorization", "Bearer " + token);
            }

            await next.Invoke(context);
        }
    }
}
