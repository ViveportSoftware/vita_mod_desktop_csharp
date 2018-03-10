using Htc.Vita.Core.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Htc.Vita.Mod.Desktop.JsonNet
{
    public class JsonFactoryImpl : JsonFactory
    {
        protected override JsonArray OnCreateJsonArray()
        {
            return new JsonArrayImpl(new JArray());
        }

        protected override JsonObject OnCreateJsonObject()
        {
            return new JsonObjectImpl(new JObject());
        }

        protected override T OnDeserializeObject<T>(string content)
        {
            return JsonConvert.DeserializeObject<T>(content);
        }

        protected override JsonArray OnGetJsonArray(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return null;
            }
            var jArray = JArray.Parse(content);
            if (jArray == null)
            {
                return null;
            }
            return new JsonArrayImpl(jArray);
        }

        protected override JsonObject OnGetJsonObject(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return null;
            }
            var jObject = JObject.Parse(content);
            if (jObject == null)
            {
                return null;
            }
            return new JsonObjectImpl(jObject);
        }

        protected override string OnSerializeObject(object content)
        {
            return JsonConvert.SerializeObject(content);
        }
    }
}
