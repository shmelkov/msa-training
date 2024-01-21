using MediatR;
using Microsoft.AspNetCore.Http;
using Portal.Common.DTOs;
using Portal.Common.Requests;

namespace Portal.Common.Behaviors
{
    public sealed class PaginationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IPagingRequest
        where TResponse : IPagedDto
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PaginationBehavior(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var pagedDto = await next();

            var httpContext = _httpContextAccessor?.HttpContext;

            if(httpContext != null)
            {
                var rangeStart = pagedDto.Offset;
                var rangeEnd = pagedDto.Offset + pagedDto.Count;
                var resourceName = GetLastPathSegment(httpContext.Request.Path);
                httpContext.Response.Headers.Add("Content-Range", $"{resourceName} {rangeStart}-{rangeEnd}/{pagedDto.TotalCount}");
            }

            return pagedDto;
        }

        private string GetLastPathSegment(string path)
        {
            return path.Split("/").Last().ToLower();
        }
    }
}
