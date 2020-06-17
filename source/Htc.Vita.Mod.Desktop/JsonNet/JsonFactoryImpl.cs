using Htc.Vita.Core.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Htc.Vita.Mod.Desktop.JsonNet
{
    /// <summary>
    /// Class JsonFactoryImpl.
    /// Implements the <see cref="JsonFactory" />
    /// </summary>
    /// <seealso cref="JsonFactory" />
    public class JsonFactoryImpl : JsonFactory
    {
        /// <inheritdoc />
        protected override JsonArray OnCreateJsonArray()
        {
            return new JsonArrayImpl(new JArray());
        }

        /// <inheritdoc />
        protected override JsonObject OnCreateJsonObject()
        {
            return new JsonObjectImpl(new JObject());
        }

        /// <inheritdoc />
        protected override T OnDeserializeObject<T>(string content)
        {
            return JsonConvert.DeserializeObject<T>(content);
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
        protected override string OnSerializeObject(object content)
        {
            return JsonConvert.SerializeObject(content);
        }
    }
}
