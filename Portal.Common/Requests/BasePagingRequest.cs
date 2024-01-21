using MediatR;
using Portal.Common.Helpers;

namespace Portal.Common.Requests
{
    public abstract class BasePagingRequest<T> : IRequest<T>, IFilteringRequest, ISortingRequest, IPagingRequest
    {
        public string? Filter { get; set; }
        public FilterOptions FilterOptions { get; set; }
        public string? Sort { get; set; }
        public SortOptions SortOptions { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
        public string? Range { get; set; }
        public PagingOptions PagingOptions { get; set; }
    }
}
