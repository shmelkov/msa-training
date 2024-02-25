using Users.Common.Helpers;

namespace Users.Common.Requests
{
    public interface ISortingRequest
    {
        public string? Sort { get; set; }

        public SortOptions SortOptions { get; }
    }
}
