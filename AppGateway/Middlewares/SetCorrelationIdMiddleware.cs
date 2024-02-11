namespace AppGateway.Middlewares
{
    public class SetCorrelationIdMiddleware
    {
        private readonly RequestDelegate next;

        public SetCorrelationIdMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var requestId = Guid.NewGuid().ToString();

            context.Request.Headers.Add("portal-correlation-id", requestId);

            await next.Invoke(context);
        }
    }
}
