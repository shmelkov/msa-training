using Users.Common.Helpers;

namespace Users.Common.Requests
{
    public interface IFilteringRequest
    {
        public string? Filter { get; set; }

        public FilterOptions FilterOptions { get; set; }
    }
}
