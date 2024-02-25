namespace Users.Common.Helpers
{
    public class FilterOptions
    {
        public List<FilterField> FilterFields { get; set; } = new List<FilterField>();
    }

    public class FilterField
    {
        public FilterField() { }

        public FilterField(string fieldName, object fieldValue) 
        {
            var fieldNameAndOperation = fieldName.Split("_", StringSplitOptions.RemoveEmptyEntries);

            this.Value = fieldValue;
            this.Name = fieldNameAndOperation[0];
            var filterOperation = fieldNameAndOperation.Last().ToLower();

            switch (filterOperation)
            {
                case "contains":
                    this.Operation = FilterOperation.Contains;
                    break;
                case "ne":
                    this.Operation = FilterOperation.NotEqual; 
                    break;
                case "gt":
                    this.Operation = FilterOperation.GreaterThan;
                    break;
                case "gte":
                    this.Operation = FilterOperation.GreaterThanOrEqual;
                    break;
                case "lt":
                    this.Operation = FilterOperation.LessThan;
                    break;
                case "lte":
                    this.Operation = FilterOperation.LessThanOrEqual;
                    break;
                default: 
                    this.Operation = FilterOperation.Equal;
                    break;

            }
        }

        public string Name { get; set; } = null!;

        public object? Value { get; set; } = null!;

        public FilterOperation Operation { get; set; }
    }

    public enum FilterOperation
    {
        Equal,
        NotEqual,
        Contains,
        GreaterThan,
        GreaterThanOrEqual,
        LessThan,
        LessThanOrEqual
    }
}
