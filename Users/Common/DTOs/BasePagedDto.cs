namespace Users.Common.DTOs
{
    public interface IPagedDto
    {
        public int Offset { get; set; }

        public int Count { get; set; }

        public int TotalCount { get; set; }
    }

    public interface IPagedDto<T> : IPagedDto
    { 
        public IEnumerable<T> Data { get; set; }
    }

    public class BasePagedDto<T> : IPagedDto<T>
    {
        public int Offset { get; set; }

        public int Count { get; set; }

        public int TotalCount { get; set; }

        public IEnumerable<T> Data { get; set; }
    }
}
