using System.Linq.Expressions;

namespace Users.Common.Helpers
{
    public class SortOptions
    {
        public List<SortField> SortFields { get; set; } = new List<SortField>();
    }

    public class SortOptions<T>
    {
        public List<SortField<T>> SortFields { get; set; } = new List<SortField<T>>();
    }

    public class SortField
    {
        public string Name { get; set; }

        public SortDirection SortDirection { get; set; } 
    }

    public class SortField<T>
    {
        public Expression<Func<T, object>> Selector { get; set; }

        public SortDirection SortDirection { get; set; }
    }

    public enum SortDirection
    {
        Ascending,
        Descending
    }
}
