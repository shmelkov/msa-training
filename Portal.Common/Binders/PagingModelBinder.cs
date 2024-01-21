using Microsoft.AspNetCore.Mvc.ModelBinding;
using Portal.Common.Helpers;
using Portal.Common.Requests;

namespace Portal.Common.Binders
{
    public class PagingModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var pagingOptions = new PagingOptions();

            var limitParamValue = bindingContext.ValueProvider.GetValue(nameof(IPagingRequest.Limit)).FirstValue;
            var offsetParamValue = bindingContext.ValueProvider.GetValue(nameof(IPagingRequest.Offset)).FirstValue;
            var rangeParamValue = bindingContext.ValueProvider.GetValue(nameof(IPagingRequest.Range)).FirstValue;

            if (limitParamValue != null)
                pagingOptions.Limit = int.Parse(limitParamValue);

            if (offsetParamValue != null)
                pagingOptions.Offset = int.Parse(offsetParamValue);

            if(rangeParamValue != null)
            {
                var parsableJson = BaseParser.DeserializeJson<List<int>>(rangeParamValue, out var parsedJson);

                if (parsableJson && parsedJson.Count == 2)
                {
                    pagingOptions.Offset = parsedJson[0];
                    pagingOptions.Limit = parsedJson[1] - parsedJson[0] + 1;
                }
                else
                {
                    throw new ApplicationException($"Parameter {nameof(IPagingRequest.Range)} is in the wrong format");
                }
            }

            bindingContext.Result = ModelBindingResult.Success(pagingOptions);

            return Task.CompletedTask;
        }
    }
}
