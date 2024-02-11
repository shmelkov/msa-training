namespace AppGateway
{
    public class AuthOptions
    {
        public static class Jwt
        {
            public static readonly string SecretKey = "SuperSecretKey123456789$%^";
        }

        public static class Cookie
        {
            public static readonly string Name = ".AspNetCore.Application.Token";
        }
    }

    public class OpendIdOptions
    {
        public string ClientId { get; set; } = "portal";

        public string Authority { get; set; } = "http://host.docker.internal:8080/realms/demoapp";

        public string ClientSecret { get; set; } = "myDemoAppPassword123456789$%^";

        public string ResponseType { get; set; } = "code";
    }
}
