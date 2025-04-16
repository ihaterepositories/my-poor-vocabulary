using System.Collections.Generic;
using Newtonsoft.Json;
using Utils.JsonBuilding.SerializingModels;

namespace Utils.JsonBuilding
{
    public static class JsonBuilder
    {
        public static string BuildJsonFromJsonReadyInputs(List<JsonReadyInputField> fields)
        {
            Dictionary<string, string> data = new();

            foreach (var field in fields)
            {
                if (string.IsNullOrEmpty(field.KeyNameForJson) || field.InputField == null) continue;
                data[field.KeyNameForJson] = field.InputField.text.Trim();
            }

            return JsonConvert.SerializeObject(data);
        }
    }
}