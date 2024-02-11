namespace AppGateway.Helpers
{
    public class AuthHelper
    {
        public void AddTokenToCookies(HttpResponse response, string token)
        {
            if (token != null)
            {
                response.Cookies.Append(AuthOptions.Cookie.Name, token,
                new CookieOptions
                {
                    SameSite = SameSiteMode.Strict,
                    Secure = true,
                    HttpOnly = true
                });

                AddHeadersForSafety(response);
            }
        }

        public void AddHeadersForSafety(HttpResponse response)
        {
            response.Headers.Add("X-Content-Type-Options", "nosniff");
            response.Headers.Add("X-Xss-Protection", "1");
            response.Headers.Add("X-Frame-Options", "DENY");
        }

        public void RemoveTokenFromCookies(HttpContext context)
        {
            context.Response.Cookies.Delete(AuthOptions.Cookie.Name);
        }
    }
}
