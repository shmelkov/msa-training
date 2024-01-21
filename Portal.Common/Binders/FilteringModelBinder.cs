using Microsoft.AspNetCore.Mvc.ModelBinding;
using Portal.Common.Extensions;
using Portal.Common.Helpers;
using Portal.Common.Requests;
using System.Globalization;
using System.Text.Json;

namespace Portal.Common.Binders
{
    public class FilteringModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var filterOptions = new FilterOptions();

            var filterParamValue = bindingContext.ValueProvider.GetValue(nameof(IFilteringRequest.Filter)).FirstValue;
            
            if (filterParamValue != null)
            {
                var parsableJson = BaseParser.DeserializeJson<Dictionary<string, object>>(filterParamValue, out var parsedJson);

                parsedJson = ConvertObjectToNativeTypeInDictionary(parsedJson);

                if (parsableJson)
                {
                    foreach (var kvp in parsedJson)
                    { 
                        filterOptions.FilterFields.Add(new FilterField(kvp.Key.FirstCharToUpper(), kvp.Value));
                    }
                }
            }

            bindingContext.Result = ModelBindingResult.Success(filterOptions);

            return Task.CompletedTask;
        }

        private Dictionary<string, object> ConvertObjectToNativeTypeInDictionary(Dictionary<string, object> dictionary) 
        {
            foreach(var kvp in dictionary)
            {
                if(kvp.Value is JsonElement element) 
                {
                    dictionary[kvp.Key] = GetObjectFromJsonElement(element);
                }
            }

            return dictionary;
        }

        private object GetObjectFromJsonElement(JsonElement element)
        {
            switch (element.ValueKind)
            {
                case JsonValueKind.Number:
                    return element.GetInt32();
                case JsonValueKind.Array:
                    return element.EnumerateArray().Select(GetObjectFromJsonElement).ToArray();
                case JsonValueKind.True:
                    return element.GetBoolean();
                case JsonValueKind.False:
                    return element.GetBoolean();
                default:
                    var str = element.GetString();
                    var isGuid = Guid.TryParse(str, out var guid);
                    var isDateTime = DateTime.TryParse(str, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out var dateTime);

                    if (isDateTime && dateTime.Kind == DateTimeKind.Unspecified)
                    {
                        dateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
                    }
                
                return isGuid ? guid : isDateTime ? dateTime : str;
            }
        }
    }
}
