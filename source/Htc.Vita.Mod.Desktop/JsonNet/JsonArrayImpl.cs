using System;
using Htc.Vita.Core.Json;
using Htc.Vita.Core.Log;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Htc.Vita.Mod.Desktop.JsonNet
{
    /// <summary>
    /// Class JsonArrayImpl.
    /// Implements the <see cref="JsonArray" />
    /// </summary>
    /// <seealso cref="JsonArray" />
    public class JsonArrayImpl : JsonArray
    {
        private readonly JArray _jArray;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonArrayImpl"/> class.
        /// </summary>
        /// <param name="jArray">The j array.</param>
        public JsonArrayImpl(JArray jArray)
        {
            _jArray = jArray;
        }

        /// <summary>
        /// Gets the inner instance.
        /// </summary>
        /// <returns>JArray.</returns>
        public JArray GetInnerInstance()
        {
            return _jArray;
        }

        /// <inheritdoc />
        protected override JsonArray OnAppendBool(bool value)
        {
            _jArray?.Add(new JValue(value));
            return this;
        }

        /// <inheritdoc />
        protected override JsonArray OnAppendDouble(double value)
        {
            _jArray?.Add(new JValue(value));
            return this;
        }

        /// <inheritdoc />
        protected override JsonArray OnAppendFloat(float value)
        {
            _jArray?.Add(new JValue(value));
            return this;
        }

        /// <inheritdoc />
        protected override JsonArray OnAppendInt(int value)
        {
            _jArray?.Add(new JValue(value));
            return this;
        }

        /// <inheritdoc />
        protected override JsonArray OnAppendLong(long value)
        {
            _jArray?.Add(new JValue(value));
            return this;
        }

        /// <inheritdoc />
        protected override JsonArray OnAppendString(string value)
        {
            _jArray?.Add(new JValue(value));
            return this;
        }

        /// <inheritdoc />
        protected override JsonArray OnAppendJsonArray(JsonArray value)
        {
            if (value == null)
            {
                return this;
            }
            try
            {
                _jArray?.Add(((JsonArrayImpl)value).GetInnerInstance());
            }
            catch (Exception)
            {
                Logger.GetInstance(typeof(JsonArrayImpl)).Error("Can not append JsonArray");
            }
            return this;
        }

        /// <inheritdoc />
        protected override JsonArray OnAppendJsonObject(JsonObject value)
        {
            if (value == null)
            {
                return this;
            }
            try
            {
                _jArray?.Add(((JsonObjectImpl)value).GetInnerInstance());
            }
            catch (Exception)
            {
                Logger.GetInstance(typeof(JsonArrayImpl)).Error("Can not append JsonObject");
            }
            return this;
        }

        /// <inheritdoc />
        protected override JsonArray OnInsertBool(
                int index,
                bool value)
        {
            _jArray?.Insert(
                    index,
                    new JValue(value)
            );
            return this;
        }

        /// <inheritdoc />
        protected override JsonArray OnInsertDouble(
                int index,
                double value)
        {
            _jArray?.Insert(
                    index,
                    new JValue(value)
            );
            return this;
        }

        /// <inheritdoc />
        protected override JsonArray OnInsertFloat(
                int index,
                float value)
        {
            _jArray?.Insert(
                    index,
                    new JValue(value)
            );
            return this;
        }

        /// <inheritdoc />
        protected override JsonArray OnInsertInt(
                int index,
                int value)
        {
            _jArray?.Insert(
                    index,
                    new JValue(value)
            );
            return this;
        }

        /// <inheritdoc />
        protected override JsonArray OnInsertLong(
                int index,
                long value)
        {
            _jArray?.Insert(
                    index,
                    new JValue(value)
            );
            return this;
        }

        /// <inheritdoc />
        protected override JsonArray OnInsertString(
                int index,
                string value)
        {
            _jArray?.Insert(
                    index,
                    new JValue(value)
            );
            return this;
        }

        /// <inheritdoc />
        protected override JsonArray OnInsertJsonArray(
                int index,
                JsonArray value)
        {
            if (value == null)
            {
                return this;
            }
            try
            {
                _jArray?.Insert(
                        index,
                        ((JsonArrayImpl)value).GetInnerInstance()
                );
            }
            catch (Exception)
            {
                Logger.GetInstance(typeof(JsonArrayImpl)).Error($"Can not put JsonArray by index: {index}");
            }
            return this;
        }

        /// <inheritdoc />
        protected override JsonArray OnInsertJsonObject(
                int index,
                JsonObject value)
        {
            if (value == null)
            {
                return this;
            }
            try
            {
                _jArray?.Insert(
                        index,
                        ((JsonObjectImpl)value).GetInnerInstance()
                );
            }
            catch (Exception)
            {
                Logger.GetInstance(typeof(JsonArrayImpl)).Error($"Can not put JsonObject by index: {index}");
            }
            return this;
        }

        /// <inheritdoc />
        protected override bool OnParseBool(
                int index,
                bool defaultValue)
        {
            try
            {
                return (bool)_jArray[index];
            }
            catch (Exception)
            {
                Logger.GetInstance(typeof(JsonArrayImpl)).Error($"Can not parse bool value by index: {index}");
                return defaultValue;
            }
        }

        /// <inheritdoc />
        protected override double OnParseDouble(
                int index,
                double defaultValue)
        {
            try
            {
                return (double)_jArray[index];
            }
            catch (Exception)
            {
                Logger.GetInstance(typeof(JsonArrayImpl)).Error($"Can not parse double value by index: {index}");
                return defaultValue;
            }
        }

        /// <inheritdoc />
        protected override float OnParseFloat(
                int index,
                float defaultValue)
        {
            try
            {
                return (float)_jArray[index];
            }
            catch (Exception)
            {
                Logger.GetInstance(typeof(JsonArrayImpl)).Error($"Can not parse float value by index: {index}");
                return defaultValue;
            }
        }

        /// <inheritdoc />
        protected override int OnParseInt(
                int index,
                int defaultValue)
        {
            try
            {
                return (int)_jArray[index];
            }
            catch (Exception)
            {
                Logger.GetInstance(typeof(JsonArrayImpl)).Error($"Can not parse int value by index: {index}");
                return defaultValue;
            }
        }

        /// <inheritdoc />
        protected override long OnParseLong(
                int index,
                long defaultValue)
        {
            try
            {
                return (long)_jArray[index];
            }
            catch (Exception)
            {
                Logger.GetInstance(typeof(JsonArrayImpl)).Error($"Can not parse long value by index: {index}");
                return defaultValue;
            }
        }

        /// <inheritdoc />
        protected override string OnParseString(
                int index,
                string defaultValue)
        {
            try
            {
                return (string)_jArray[index];
            }
            catch (Exception)
            {
                Logger.GetInstance(typeof(JsonArrayImpl)).Error($"Can not parse string value by index: {index}");
                return defaultValue;
            }
        }

        /// <inheritdoc />
        protected override JsonArray OnParseJsonArray(int index)
        {
            try
            {
                return new JsonArrayImpl((JArray)_jArray[index]);
            }
            catch (Exception)
            {
                Logger.GetInstance(typeof(JsonArrayImpl)).Error($"Can not parse JArray value by index: {index}");
            }
            return null;
        }

        /// <inheritdoc />
        protected override JsonObject OnParseJsonObject(int index)
        {
            try
            {
                return new JsonObjectImpl((JObject)_jArray[index]);
            }
            catch (Exception)
            {
                Logger.GetInstance(typeof(JsonArrayImpl)).Error($"Can not parse JObject value by index: {index}");
            }
            return null;
        }

        /// <inheritdoc />
        protected override int OnSize()
        {
            return _jArray?.Count ?? 0;
        }

        /// <inheritdoc />
        protected override string OnToPrettyString()
        {
            return _jArray?.ToString(Formatting.Indented) ?? "";
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>System.String.</returns>
        public override string ToString()
        {
            return _jArray?.ToString(Formatting.None) ?? "";
        }
    }
}
