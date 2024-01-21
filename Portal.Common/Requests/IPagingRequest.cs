using Portal.Common.Helpers;

namespace Portal.Common.Requests
{
    public interface IPagingRequest
    {
        public int Limit { get; set; }
        
        public int Offset { get; set; }

        public string? Range { get; set; }

        public PagingOptions PagingOptions { get; set; }
    }
}
