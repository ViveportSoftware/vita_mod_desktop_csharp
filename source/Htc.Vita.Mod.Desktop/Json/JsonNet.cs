using System;
using System.Collections.Generic;
using System.Linq;
using Htc.Vita.Core.Log;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Htc.Vita.Mod.Desktop.Json
{
    /// <summary>
    /// Class JsonNet.
    /// </summary>
    public static class JsonNet
    {
        /// <summary>
        /// Class JsonArray.
        /// Implements the <see cref="Core.Json.JsonArray" />
        /// </summary>
        /// <seealso cref="Core.Json.JsonArray" />
        public class JsonArray : Core.Json.JsonArray
        {
            private readonly JArray _jArray;

            /// <summary>
            /// Initializes a new instance of the <see cref="JsonArray" /> class.
            /// </summary>
            /// <param name="jArray">The j array.</param>
            public JsonArray(JArray jArray)
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
            protected override Core.Json.JsonArray OnAppendBool(bool value)
            {
                _jArray?.Add(new JValue(value));
                return this;
            }

            /// <inheritdoc />
            protected override Core.Json.JsonArray OnAppendDouble(double value)
            {
                _jArray?.Add(new JValue(value));
                return this;
            }

            /// <inheritdoc />
            protected override Core.Json.JsonArray OnAppendFloat(float value)
            {
                _jArray?.Add(new JValue(value));
                return this;
            }

            /// <inheritdoc />
            protected override Core.Json.JsonArray OnAppendInt(int value)
            {
                _jArray?.Add(new JValue(value));
                return this;
            }

            /// <inheritdoc />
            protected override Core.Json.JsonArray OnAppendLong(long value)
            {
                _jArray?.Add(new JValue(value));
                return this;
            }

            /// <inheritdoc />
            protected override Core.Json.JsonArray OnAppendString(string value)
            {
                _jArray?.Add(new JValue(value));
                return this;
            }

            /// <inheritdoc />
            protected override Core.Json.JsonArray OnAppendJsonArray(Core.Json.JsonArray value)
            {
                if (value == null)
                {
                    return this;
                }
                try
                {
                    _jArray?.Add(((JsonArray)value).GetInnerInstance());
                }
                catch (Exception)
                {
                    Logger.GetInstance(typeof(JsonArray)).Error("Can not append JsonArray");
                }
                return this;
            }

            /// <inheritdoc />
            protected override Core.Json.JsonArray OnAppendJsonObject(Core.Json.JsonObject value)
            {
                if (value == null)
                {
                    return this;
                }
                try
                {
                    _jArray?.Add(((JsonObject)value).GetInnerInstance());
                }
                catch (Exception)
                {
                    Logger.GetInstance(typeof(JsonArray)).Error("Can not append JsonObject");
                }
                return this;
            }

            /// <inheritdoc />
            protected override Core.Json.JsonArray OnInsertBool(
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
            protected override Core.Json.JsonArray OnInsertDouble(
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
            protected override Core.Json.JsonArray OnInsertFloat(
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
            protected override Core.Json.JsonArray OnInsertInt(
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
            protected override Core.Json.JsonArray OnInsertLong(
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
            protected override Core.Json.JsonArray OnInsertString(
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
            protected override Core.Json.JsonArray OnInsertJsonArray(
                    int index,
                    Core.Json.JsonArray value)
            {
                if (value == null)
                {
                    return this;
                }
                try
                {
                    _jArray?.Insert(
                            index,
                            ((JsonArray)value).GetInnerInstance()
                    );
                }
                catch (Exception)
                {
                    Logger.GetInstance(typeof(JsonArray)).Error($"Can not put JsonArray by index: {index}");
                }
                return this;
            }

            /// <inheritdoc />
            protected override Core.Json.JsonArray OnInsertJsonObject(
                    int index,
                    Core.Json.JsonObject value)
            {
                if (value == null)
                {
                    return this;
                }
                try
                {
                    _jArray?.Insert(
                            index,
                            ((JsonObject)value).GetInnerInstance()
                    );
                }
                catch (Exception)
                {
                    Logger.GetInstance(typeof(JsonArray)).Error($"Can not put JsonObject by index: {index}");
                }
                return this;
            }

            /// <inheritdoc />
            protected override bool OnParseBool(
                    int index,
                    bool defaultValue)
            {
                if (_jArray == null || _jArray.Count <= index)
                {
                    return defaultValue;
                }

                try
                {
                    return (bool)_jArray[index];
                }
                catch (Exception)
                {
                    Logger.GetInstance(typeof(JsonArray)).Error($"Can not parse bool value by index: {index}");
                    return defaultValue;
                }
            }

            /// <inheritdoc />
            protected override double OnParseDouble(
                    int index,
                    double defaultValue)
            {
                if (_jArray == null || _jArray.Count <= index)
                {
                    return defaultValue;
                }

                try
                {
                    return (double)_jArray[index];
                }
                catch (Exception)
                {
                    Logger.GetInstance(typeof(JsonArray)).Error($"Can not parse double value by index: {index}");
                    return defaultValue;
                }
            }

            /// <inheritdoc />
            protected override float OnParseFloat(
                    int index,
                    float defaultValue)
            {
                if (_jArray == null || _jArray.Count <= index)
                {
                    return defaultValue;
                }

                try
                {
                    return (float)_jArray[index];
                }
                catch (Exception)
                {
                    Logger.GetInstance(typeof(JsonArray)).Error($"Can not parse float value by index: {index}");
                    return defaultValue;
                }
            }

            /// <inheritdoc />
            protected override int OnParseInt(
                    int index,
                    int defaultValue)
            {
                if (_jArray == null || _jArray.Count <= index)
                {
                    return defaultValue;
                }

                try
                {
                    return (int)_jArray[index];
                }
                catch (Exception)
                {
                    Logger.GetInstance(typeof(JsonArray)).Error($"Can not parse int value by index: {index}");
                    return defaultValue;
                }
            }

            /// <inheritdoc />
            protected override long OnParseLong(
                    int index,
                    long defaultValue)
            {
                if (_jArray == null || _jArray.Count <= index)
                {
                    return defaultValue;
                }

                try
                {
                    return (long)_jArray[index];
                }
                catch (Exception)
                {
                    Logger.GetInstance(typeof(JsonArray)).Error($"Can not parse long value by index: {index}");
                    return defaultValue;
                }
            }

            /// <inheritdoc />
            protected override string OnParseString(
                    int index,
                    string defaultValue)
            {
                if (_jArray == null || _jArray.Count <= index)
                {
                    return defaultValue;
                }

                try
                {
                    return (string)_jArray[index];
                }
                catch (Exception)
                {
                    Logger.GetInstance(typeof(JsonArray)).Error($"Can not parse string value by index: {index}");
                    return defaultValue;
                }
            }

            /// <inheritdoc />
            protected override Core.Json.JsonArray OnParseJsonArray(int index)
            {
                if (_jArray == null || _jArray.Count <= index)
                {
                    return null;
                }

                try
                {
                    return new JsonArray((JArray)_jArray[index]);
                }
                catch (Exception)
                {
                    Logger.GetInstance(typeof(JsonArray)).Error($"Can not parse JArray value by index: {index}");
                }
                return null;
            }

            /// <inheritdoc />
            protected override Core.Json.JsonObject OnParseJsonObject(int index)
            {
                if (_jArray == null || _jArray.Count <= index)
                {
                    return null;
                }

                try
                {
                    return new JsonObject((JObject)_jArray[index]);
                }
                catch (Exception)
                {
                    Logger.GetInstance(typeof(JsonArray)).Error($"Can not parse JObject value by index: {index}");
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

        /// <summary>
        /// Class JsonFactory.
        /// Implements the <see cref="Core.Json.JsonFactory" />
        /// </summary>
        /// <seealso cref="Core.Json.JsonFactory" />
        public class JsonFactory : Core.Json.JsonFactory
        {
            /// <inheritdoc />
            protected override Core.Json.JsonArray OnCreateJsonArray()
            {
                return new JsonArray(new JArray());
            }

            /// <inheritdoc />
            protected override Core.Json.JsonObject OnCreateJsonObject()
            {
                return new JsonObject(new JObject());
            }

            /// <inheritdoc />
            protected override T OnDeserializeObject<T>(string content)
            {
                return JsonConvert.DeserializeObject<T>(content);
            }

            /// <inheritdoc />
            protected override Core.Json.JsonArray OnGetJsonArray(string content)
            {
                if (string.IsNullOrWhiteSpace(content))
                {
                    return null;
                }
                return new JsonArray(JArray.Parse(content));
            }

            /// <inheritdoc />
            protected override Core.Json.JsonObject OnGetJsonObject(string content)
            {
                if (string.IsNullOrWhiteSpace(content))
                {
                    return null;
                }
                return new JsonObject(JObject.Parse(content));
            }

            /// <inheritdoc />
            protected override string OnSerializeObject(object content)
            {
                return JsonConvert.SerializeObject(content);
            }
        }

        /// <summary>
        /// Class JsonObject.
        /// Implements the <see cref="Core.Json.JsonObject" />
        /// </summary>
        /// <seealso cref="Core.Json.JsonObject" />
        public class JsonObject : Core.Json.JsonObject
        {
            private readonly JObject _jObject;

            /// <summary>
            /// Initializes a new instance of the <see cref="JsonObject" /> class.
            /// </summary>
            /// <param name="jObject">The j object.</param>
            public JsonObject(JObject jObject)
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
                return _jObject?.Properties().Select(p => p.Name).ToList();
            }

            /// <inheritdoc />
            protected override bool OnHasKey(string key)
            {
                if (_jObject == null)
                {
                    return false;
                }
                if (string.IsNullOrWhiteSpace(key))
                {
                    return false;
                }

                try
                {
                    return _jObject.Properties().Any(property => key.Equals(property.Name));
                }
                catch (Exception)
                {
                    Logger.GetInstance(typeof(JsonObject)).Error($"Can not check if the object has key: {key}");
                }
                return false;
            }

            /// <inheritdoc />
            protected override bool OnParseBool(
                    string key,
                    bool defaultValue)
            {
                if (_jObject == null)
                {
                    return defaultValue;
                }

                try
                {
                    return (bool)_jObject[key];
                }
                catch (Exception)
                {
                    Logger.GetInstance(typeof(JsonObject)).Error($"Can not parse bool value by key: {key}");
                    return defaultValue;
                }
            }

            /// <inheritdoc />
            protected override double OnParseDouble(
                    string key,
                    double defaultValue)
            {
                if (_jObject == null)
                {
                    return defaultValue;
                }

                try
                {
                    return (double)_jObject[key];
                }
                catch (Exception)
                {
                    Logger.GetInstance(typeof(JsonObject)).Error($"Can not parse double value by key: {key}");
                    return defaultValue;
                }
            }

            /// <inheritdoc />
            protected override float OnParseFloat(
                    string key,
                    float defaultValue)
            {
                if (_jObject == null)
                {
                    return defaultValue;
                }

                try
                {
                    return (float)_jObject[key];
                }
                catch (Exception)
                {
                    Logger.GetInstance(typeof(JsonObject)).Error($"Can not parse float value by key: {key}");
                    return defaultValue;
                }
            }

            /// <inheritdoc />
            protected override int OnParseInt(
                    string key,
                    int defaultValue)
            {
                if (_jObject == null)
                {
                    return defaultValue;
                }

                try
                {
                    return (int)_jObject[key];
                }
                catch (Exception)
                {
                    Logger.GetInstance(typeof(JsonObject)).Error($"Can not parse int value by key: {key}");
                    return defaultValue;
                }
            }

            /// <inheritdoc />
            protected override long OnParseLong(
                    string key,
                    long defaultValue)
            {
                if (_jObject == null)
                {
                    return defaultValue;
                }

                try
                {
                    return (long)_jObject[key];
                }
                catch (Exception)
                {
                    Logger.GetInstance(typeof(JsonObject)).Error($"Can not parse long value by key: {key}");
                    return defaultValue;
                }
            }

            /// <inheritdoc />
            protected override string OnParseString(
                    string key,
                    string defaultValue)
            {
                if (_jObject == null)
                {
                    return defaultValue;
                }

                try
                {
                    return (string)_jObject[key];
                }
                catch (Exception)
                {
                    Logger.GetInstance(typeof(JsonObject)).Error($"Can not parse string value by key: {key}");
                    return defaultValue;
                }
            }

            /// <inheritdoc />
            protected override Core.Json.JsonArray OnParseJsonArray(string key)
            {
                if (_jObject == null)
                {
                    return null;
                }

                try
                {
                    var jArray = (JArray)_jObject[key];
                    if (jArray != null)
                    {
                        return new JsonArray(jArray);
                    }
                }
                catch (Exception)
                {
                    Logger.GetInstance(typeof(JsonObject)).Error($"Can not parse JArray value by key: {key}");
                }
                return null;
            }

            /// <inheritdoc />
            protected override Core.Json.JsonObject OnParseJsonObject(string key)
            {
                if (_jObject == null)
                {
                    return null;
                }

                try
                {
                    var jObject = (JObject)_jObject[key];
                    if (jObject != null)
                    {
                        return new JsonObject(jObject);
                    }
                }
                catch (Exception)
                {
                    Logger.GetInstance(typeof(JsonObject)).Error($"Can not parse JObject value by key: {key}");
                }
                return null;
            }

            /// <inheritdoc />
            protected override Core.Json.JsonObject OnPutBool(
                    string key,
                    bool value)
            {
                if (_jObject == null)
                {
                    return this;
                }

                if (!string.IsNullOrWhiteSpace(key))
                {
                    _jObject[key] = value;
                }
                return this;
            }

            /// <inheritdoc />
            protected override Core.Json.JsonObject OnPutDouble(
                    string key,
                    double value)
            {
                if (_jObject == null)
                {
                    return this;
                }

                if (!string.IsNullOrWhiteSpace(key))
                {
                    _jObject[key] = value;
                }
                return this;
            }

            /// <inheritdoc />
            protected override Core.Json.JsonObject OnPutFloat(
                    string key,
                    float value)
            {
                if (_jObject == null)
                {
                    return this;
                }

                if (!string.IsNullOrWhiteSpace(key))
                {
                    _jObject[key] = value;
                }
                return this;
            }

            /// <inheritdoc />
            protected override Core.Json.JsonObject OnPutInt(
                    string key,
                    int value)
            {
                if (_jObject == null)
                {
                    return this;
                }

                if (!string.IsNullOrWhiteSpace(key))
                {
                    _jObject[key] = value;
                }
                return this;
            }

            /// <inheritdoc />
            protected override Core.Json.JsonObject OnPutLong(
                    string key,
                    long value)
            {
                if (_jObject == null)
                {
                    return this;
                }

                if (!string.IsNullOrWhiteSpace(key))
                {
                    _jObject[key] = value;
                }
                return this;
            }

            /// <inheritdoc />
            protected override Core.Json.JsonObject OnPutString(
                    string key,
                    string value)
            {
                if (_jObject == null)
                {
                    return this;
                }

                if (!string.IsNullOrWhiteSpace(key))
                {
                    _jObject[key] = value;
                }
                return this;
            }

            /// <inheritdoc />
            protected override Core.Json.JsonObject OnPutJsonArray(
                    string key,
                    Core.Json.JsonArray value)
            {
                if (_jObject == null)
                {
                    return this;
                }

                if (string.IsNullOrWhiteSpace(key) || value == null)
                {
                    return this;
                }
                try
                {
                    _jObject[key] = ((JsonArray)value).GetInnerInstance();
                }
                catch (Exception)
                {
                    Logger.GetInstance(typeof(JsonObject)).Error($"Can not put JsonArray by key: {key}");
                }
                return this;
            }

            /// <inheritdoc />
            protected override Core.Json.JsonObject OnPutJsonObject(
                    string key,
                    Core.Json.JsonObject value)
            {
                if (_jObject == null)
                {
                    return this;
                }

                if (string.IsNullOrWhiteSpace(key) || value == null)
                {
                    return this;
                }
                try
                {
                    _jObject[key] = ((JsonObject)value).GetInnerInstance();
                }
                catch (Exception)
                {
                    Logger.GetInstance(typeof(JsonObject)).Error($"Can not put JsonObject by key: {key}");
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
}
