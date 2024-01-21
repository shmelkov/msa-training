using Microsoft.OpenApi.Models;
using Portal.Common.Requests;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Portal.Common.Filters
{
    public class BaseRequestParametersFilter : IOperationFilter
    {
        private static Dictionary<Type, string[]> _typesAndPropertiesToRemove = new Dictionary<Type, string[]>
        {
            { typeof(ISortingRequest), new string[] { $"{nameof(ISortingRequest.SortOptions)}.{nameof(ISortingRequest.SortOptions.SortFields)}" } },
            
            { typeof(IFilteringRequest), new string[] { $"{nameof(IFilteringRequest.FilterOptions)}.{nameof(IFilteringRequest.FilterOptions.FilterFields)}" } },
            
            { typeof(IPagingRequest), new string[] { $"{nameof(IPagingRequest.PagingOptions)}.{nameof(IPagingRequest.PagingOptions.Limit)}",
                                                     $"{nameof(IPagingRequest.PagingOptions)}.{nameof(IPagingRequest.PagingOptions.Offset)}" } }
        };

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            foreach(var type in _typesAndPropertiesToRemove)
            {
                var parameterInfos = context.MethodInfo.GetParameters()
                    .Where(i => type.Key.IsAssignableFrom(i.ParameterType));

                foreach (var parameterInfo in parameterInfos)
                {
                    foreach (var propertyToRemove in type.Value)
                    {
                        var parameter = operation.Parameters.FirstOrDefault(p => p.Name == propertyToRemove);

                        if (parameter != null)
                        {
                            operation.Parameters.Remove(parameter);
                        }
                    }
                }
            }
        }
    }
}
