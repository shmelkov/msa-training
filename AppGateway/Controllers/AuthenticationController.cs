using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace AppGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(ILogger<AuthenticationController> logger)
        {
            _logger = logger;
        }

        [HttpGet("login")]
        public async Task Login(string returnUrl = "/")
        {
            await this.HttpContext.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme,
                new AuthenticationProperties { RedirectUri = returnUrl });
        }

        [HttpGet("logout")]
        public IActionResult Logout(string returnUrl = "/")
        {
            return new SignOutResult(
            new[] {
                OpenIdConnectDefaults.AuthenticationScheme,
                CookieAuthenticationDefaults.AuthenticationScheme
            }, new AuthenticationProperties { RedirectUri = returnUrl });
        }
    }
}