using LinqKit;
using Microsoft.EntityFrameworkCore;
using Users.Common.Helpers;

namespace Users.Common.Extensions
{
    public static class IQueryableExtensions
    {
        public static IOrderedQueryable<T> ApplySortOptions<T>(this IQueryable<T> queryable, SortOptions<T> sortOptions) where T : class
        {
            var result = queryable.OrderBy(p => 0);

            foreach(var sortField in sortOptions.SortFields)
            {
                result = sortField.SortDirection == SortDirection.Ascending ?
                            result.ThenBy(i => sortField.Selector) :
                            result.ThenByDescending(i => sortField.Selector);
            }

            return result;
        }

        public static IQueryable<T> ApplySortOptions<T>(this IQueryable<T> queryable, SortOptions sortOptions) where T : class
        {
            var result = queryable.OrderBy(p => 0);

            foreach (var sortField in sortOptions.SortFields)
            {
                var propertyName = sortField.Name.FirstCharToUpper();

                if (!CheckIfPropertyExists<T>(propertyName, out var propertyType))
                    throw new ApplicationException($"'{propertyName}' doesn't exist");

                result = sortField.SortDirection == SortDirection.Ascending ?
                            result.ThenBy(i => EF.Property<object>(i, propertyName)) :
                            result.ThenByDescending(i => EF.Property<object>(i, propertyName));
            }

            return result;
        }

        public static IQueryable<T> ApplyPagingOptions<T>(this IQueryable<T> queryable, PagingOptions pagingOptions) where T : class
        {
            return queryable.Skip(pagingOptions.Offset).Take(pagingOptions.Limit);
        }

        public static IQueryable<T> ApplyFilterOptions<T>(this IQueryable<T> queryable, FilterOptions filterOptions) where T : class
        {
            foreach(var filterField in filterOptions.FilterFields)
            {
                var propertyName = filterField.Name.FirstCharToUpper();

                if (!CheckIfPropertyExists<T>(propertyName, out var propertyType))
                    throw new ApplicationException($"'{propertyName}' doesn't exist");

                if (filterField.Operation == FilterOperation.Contains && propertyType != typeof(string))
                    throw new ApplicationException($"Operation 'Contains' can not be applied to field '{propertyName}'");
                

                if (filterField.Value is object[] array)
                {
                    if(filterField.Operation == FilterOperation.Equal)
                    {                 
                        if (propertyType == typeof(Guid))
                        {
                            var guidArray = array.OfType<Guid>();
                            queryable = queryable.Where(i => guidArray.Any(p => EF.Property<Guid>(i, propertyName) == p));
                        }
                        else
                            queryable = queryable.Where(i => array.Any(p => EF.Property<object>(i, propertyName) == p));
                    }
                    else if(filterField.Operation == FilterOperation.NotEqual)
                    {
                        queryable = queryable.Where(i => array.All(p => EF.Property<object>(i, propertyName) != p));
                    }
                    else if (filterField.Operation == FilterOperation.Contains && propertyType == typeof(string))
                    {   
                        var predicate = PredicateBuilder.New<T>();

                        foreach (var stringValue in array.OfType<string>())
                        {
                            predicate.Or(i => EF.Property<string>(i, propertyName) != null && EF.Property<string>(i, propertyName).Contains(stringValue));
                        }

                        queryable = queryable.Where(predicate);
                    }
                }
                else
                {
                    if (filterField.Operation == FilterOperation.Equal)
                    {
                        if (filterField.Value is Guid guidValue && propertyType == typeof(Guid))
                            queryable = queryable.Where(i => EF.Property<Guid>(i, propertyName) == guidValue);
                        else
                            queryable = queryable.Where(i => EF.Property<object>(i, propertyName) == filterField.Value);
                    }
                    else if (filterField.Operation == FilterOperation.NotEqual)
                    {
                        queryable = queryable.Where(i => EF.Property<object>(i, propertyName) != filterField.Value);
                    }
                    else if (filterField.Operation == FilterOperation.Contains && propertyType == typeof(string) && filterField.Value is string stringValue)
                    {
                        queryable = queryable.Where(i => EF.Property<string>(i, propertyName).Contains(stringValue));
                    }
                    else if (filterField.Operation == FilterOperation.GreaterThan && propertyType == typeof(DateTime) && filterField.Value is DateTime dateTimeValueForGt)
                    {
                        queryable = queryable.Where(i => EF.Property<DateTime>(i, propertyName) > dateTimeValueForGt);
                    }
                    else if (filterField.Operation == FilterOperation.GreaterThanOrEqual && propertyType == typeof(DateTime) && filterField.Value is DateTime dateTimeValueForGte)
                    {
                        queryable = queryable.Where(i => EF.Property<DateTime>(i, propertyName) >= dateTimeValueForGte);
                    }
                    else if (filterField.Operation == FilterOperation.LessThan && propertyType == typeof(DateTime) && filterField.Value is DateTime dateTimeValueForLt)
                    {
                        queryable = queryable.Where(i => EF.Property<DateTime>(i, propertyName) < dateTimeValueForLt);
                    }
                    else if (filterField.Operation == FilterOperation.LessThanOrEqual && propertyType == typeof(DateTime) && filterField.Value is DateTime dateTimeValueLte)
                    {
                        queryable = queryable.Where(i => EF.Property<DateTime>(i, propertyName) <= dateTimeValueLte);
                    }
                }
            }

            return queryable;
        }

        private static bool CheckIfPropertyExists<T>(string propertyName, out Type? propertyType)
        {
            var propertyInfo = typeof(T).GetProperty(propertyName);

            propertyType = propertyInfo?.PropertyType;

            return propertyInfo != null;
        }

    }
}
