using System;
using System.Collections.Generic;
using System.Linq;
using Htc.Vita.Core.Json;
using Htc.Vita.Core.Log;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Htc.Vita.Mod.Desktop.JsonNet
{
    /// <summary>
    /// Class JsonObjectImpl.
    /// Implements the <see cref="JsonObject" />
    /// </summary>
    /// <seealso cref="JsonObject" />
    public class JsonObjectImpl : JsonObject
    {
        private readonly JObject _jObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonObjectImpl"/> class.
        /// </summary>
        /// <param name="jObject">The j object.</param>
        public JsonObjectImpl(JObject jObject)
        {
            _jObject = jObject;
        }

        /// <summary>
        /// Gets the inner instance.
        /// </summary>
        /// <returns>JObject.</returns>
        public JObject GetInnerInstance()
        {
            return _jObject;
        }

        /// <inheritdoc />
        protected override ICollection<string> OnAllKeys()
        {
            return _jObject.Properties().Select(p => p.Name).ToList();
        }

        /// <inheritdoc />
        protected override bool OnHasKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return false;
            }
            return _jObject.Properties().Any(property => key.Equals(property.Name));
        }

        /// <inheritdoc />
        protected override bool OnParseBool(
                string key,
                bool defaultValue)
        {
            var result = defaultValue;
            try
            {
                result = (bool)_jObject[key];
            }
            catch (Exception)
            {
                Logger.GetInstance(typeof(JsonObjectImpl)).Error($"Can not parse bool value by key: {key}");
            }
            return result;
        }

        /// <inheritdoc />
        protected override double OnParseDouble(
                string key,
                double defaultValue)
        {
            var result = defaultValue;
            try
            {
                result = (double)_jObject[key];
            }
            catch (Exception)
            {
                Logger.GetInstance(typeof(JsonObjectImpl)).Error($"Can not parse double value by key: {key}");
            }
            return result;
        }

        /// <inheritdoc />
        protected override float OnParseFloat(
                string key,
                float defaultValue)
        {
            var result = defaultValue;
            try
            {
                result = (float)_jObject[key];
            }
            catch (Exception)
            {
                Logger.GetInstance(typeof(JsonObjectImpl)).Error($"Can not parse float value by key: {key}");
            }
            return result;
        }

        /// <inheritdoc />
        protected override int OnParseInt(
                string key,
                int defaultValue)
        {
            var result = defaultValue;
            try
            {
                result = (int)_jObject[key];
            }
            catch (Exception)
            {
                Logger.GetInstance(typeof(JsonObjectImpl)).Error($"Can not parse int value by key: {key}");
            }
            return result;
        }

        /// <inheritdoc />
        protected override long OnParseLong(
                string key,
                long defaultValue)
        {
            var result = defaultValue;
            try
            {
                result = (long)_jObject[key];
            }
            catch (Exception)
            {
                Logger.GetInstance(typeof(JsonObjectImpl)).Error($"Can not parse long value by key: {key}");
            }
            return result;
        }

        /// <inheritdoc />
        protected override string OnParseString(
                string key,
                string defaultValue)
        {
            var result = defaultValue;
            try
            {
                result = (string)_jObject[key];
            }
            catch (Exception)
            {
                Logger.GetInstance(typeof(JsonObjectImpl)).Error($"Can not parse string value by key: {key}");
            }
            return result;
        }

        /// <inheritdoc />
        protected override JsonArray OnParseJsonArray(string key)
        {
            try
            {
                return new JsonArrayImpl((JArray)_jObject[key]);
            }
            catch (Exception)
            {
                Logger.GetInstance(typeof(JsonObjectImpl)).Error($"Can not parse JArray value by key: {key}");
            }
            return null;
        }

        /// <inheritdoc />
        protected override JsonObject OnParseJsonObject(string key)
        {
            try
            {
                return new JsonObjectImpl((JObject)_jObject[key]);
            }
            catch (Exception)
            {
                Logger.GetInstance(typeof(JsonObjectImpl)).Error($"Can not parse JObject value by key: {key}");
            }
            return null;
        }

        /// <inheritdoc />
        protected override JsonObject OnPutBool(
                string key,
                bool value)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                _jObject[key] = value;
            }
            return this;
        }

        /// <inheritdoc />
        protected override JsonObject OnPutDouble(
                string key,
                double value)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                _jObject[key] = value;
            }
            return this;
        }

        /// <inheritdoc />
        protected override JsonObject OnPutFloat(
                string key,
                float value)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                _jObject[key] = value;
            }
            return this;
        }

        /// <inheritdoc />
        protected override JsonObject OnPutInt(
                string key,
                int value)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                _jObject[key] = value;
            }
            return this;
        }

        /// <inheritdoc />
        protected override JsonObject OnPutLong(
                string key,
                long value)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                _jObject[key] = value;
            }
            return this;
        }

        /// <inheritdoc />
        protected override JsonObject OnPutString(
                string key,
                string value)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                _jObject[key] = value;
            }
            return this;
        }

        /// <inheritdoc />
        protected override JsonObject OnPutJsonArray(
                string key,
                JsonArray value)
        {
            if (string.IsNullOrWhiteSpace(key) || value == null)
            {
                return this;
            }
            try
            {
                _jObject[key] = ((JsonArrayImpl)value).GetInnerInstance();
            }
            catch (Exception)
            {
                Logger.GetInstance(typeof(JsonObjectImpl)).Error($"Can not put JsonArray by key: {key}");
            }
            return this;
        }

        /// <inheritdoc />
        protected override JsonObject OnPutJsonObject(
                string key,
                JsonObject value)
        {
            if (string.IsNullOrWhiteSpace(key) || value == null)
            {
                return this;
            }
            try
            {
                _jObject[key] = ((JsonObjectImpl)value).GetInnerInstance();
            }
            catch (Exception)
            {
                Logger.GetInstance(typeof(JsonObjectImpl)).Error($"Can not put JsonObject by key: {key}");
            }
            return this;
        }

        /// <inheritdoc />
        protected override string OnToPrettyString()
        {
            return _jObject?.ToString(Formatting.Indented) ?? "";
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>System.String.</returns>
        public override string ToString()
        {
            return _jObject?.ToString(Formatting.None) ?? "";
        }
    }
}
