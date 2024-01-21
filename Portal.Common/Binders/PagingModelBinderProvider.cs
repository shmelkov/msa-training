using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Portal.Common.Requests;

namespace Portal.Common.Binders
{
    public class PagingModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if(typeof(IPagingRequest).IsAssignableFrom(context.Metadata.ContainerType) && context.Metadata.Name == nameof(IPagingRequest.PagingOptions))
            {
                return new BinderTypeModelBinder(typeof(PagingModelBinder));
            }

            return null;
        }
    }
}
