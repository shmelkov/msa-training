using Microsoft.AspNetCore.Mvc.ModelBinding;
using Portal.Common.Helpers;
using Portal.Common.Requests;

namespace Portal.Common.Binders
{
    public class SortingModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var sortOptions = new SortOptions();

            var sortParam = bindingContext.ValueProvider.GetValue(nameof(ISortingRequest.Sort));

            foreach(var sortParamValue in sortParam.Values)
            {
                if(sortParamValue != null && !String.IsNullOrWhiteSpace(sortParamValue))
                {
                    var sortFieldsFromJson = GetSortFieldsFromJson(sortParamValue);

                    if(sortFieldsFromJson.Count > 0)
                    {
                        sortOptions.SortFields.AddRange(sortFieldsFromJson);
                    }
                    else
                    {
                        var sortFieldsFromString = GetSortFieldsFromString(sortParamValue);

                        sortOptions.SortFields.AddRange(sortFieldsFromString);
                    }
                } 
            }

            bindingContext.Result = ModelBindingResult.Success(sortOptions);

            return Task.CompletedTask;
        }

        private List<SortField> GetSortFieldsFromJson(string json)
        {
            var result = new List<SortField>();

            var parsableJson = BaseParser.DeserializeJson<List<string>>(json, out var parsedJson);

            if (parsableJson && parsedJson.Count == 2)
            {
                result.Add(new SortField { Name = parsedJson[0], SortDirection = parsedJson[1].ToUpper() == "DESC" ? SortDirection.Descending : SortDirection.Ascending });

            }

            return result;
        }

        private List<SortField> GetSortFieldsFromString(string sortString)
        {
            var result = new List<SortField>();

            var fieldsToSort = sortString.Split(',', StringSplitOptions.RemoveEmptyEntries);

            foreach (var field in fieldsToSort)
            {
                var sortDirection = field[0] == '-' ? SortDirection.Descending : SortDirection.Ascending;

                var fieldName = field[0] == '-' || field[0] == '+' ? field.Substring(1) : field;

                if (!String.IsNullOrWhiteSpace(fieldName))
                {
                    result.Add(new SortField { Name = fieldName, SortDirection = sortDirection });
                }
            }

            return result;
        }
    }
}
