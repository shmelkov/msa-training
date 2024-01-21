using Portal.Common.Helpers;

namespace Portal.Common.Requests
{
    public interface ISortingRequest
    {
        public string? Sort { get; set; }

        public SortOptions SortOptions { get; }
    }
}
