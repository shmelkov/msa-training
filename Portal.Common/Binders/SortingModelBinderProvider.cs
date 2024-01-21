using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Portal.Common.Requests;

namespace Portal.Common.Binders
{
    public class SortingModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if(typeof(ISortingRequest).IsAssignableFrom(context.Metadata.ContainerType) && context.Metadata.Name == nameof(ISortingRequest.SortOptions))
            {
                return new BinderTypeModelBinder(typeof(SortingModelBinder));
            }

            return null;
        }
    }
}
