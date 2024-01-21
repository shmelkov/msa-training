using Portal.Common.Helpers;

namespace Portal.Common.Requests
{
    public interface IFilteringRequest
    {
        public string? Filter { get; set; }

        public FilterOptions FilterOptions { get; set; }
    }
}
