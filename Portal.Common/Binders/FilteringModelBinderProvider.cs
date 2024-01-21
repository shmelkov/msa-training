using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Portal.Common.Requests;

namespace Portal.Common.Binders
{
    public class FilteringModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (typeof(IFilteringRequest).IsAssignableFrom(context.Metadata.ContainerType) && context.Metadata.Name == nameof(IFilteringRequest.FilterOptions))
            {
                return new BinderTypeModelBinder(typeof(FilteringModelBinder));
            }

            return null;
        }
    }
}
