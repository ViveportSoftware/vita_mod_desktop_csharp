using System;
using Htc.Vita.Core.Json;
using Htc.Vita.Core.Log;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Htc.Vita.Mod.Desktop.JsonNet
{
    public class JsonArrayImpl : JsonArray
    {
        private readonly JArray _jArray;
        private readonly Logger _logger;

        public JsonArrayImpl(JArray jArray)
        {
            _jArray = jArray;
            _logger = Logger.GetInstance();
        }

        public JArray GetInnerInstance()
        {
            return _jArray;
        }

        protected override JsonArray OnAppendBool(bool value)
        {
            _jArray?.Add(new JValue(value));
            return this;
        }

        protected override JsonArray OnAppendDouble(double value)
        {
            _jArray?.Add(new JValue(value));
            return this;
        }

        protected override JsonArray OnAppendFloat(float value)
        {
            _jArray?.Add(new JValue(value));
            return this;
        }

        protected override JsonArray OnAppendInt(int value)
        {
            _jArray?.Add(new JValue(value));
            return this;
        }

        protected override JsonArray OnAppendLong(long value)
        {
            _jArray?.Add(new JValue(value));
            return this;
        }

        protected override JsonArray OnAppendString(string value)
        {
            _jArray?.Add(new JValue(value));
            return this;
        }

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
                _logger.Error("Can not append JsonArray");
            }
            return this;
        }

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
                _logger.Error("Can not append JsonObject");
            }
            return this;
        }

        protected override JsonArray OnInsertBool(int index, bool value)
        {
            _jArray?.Insert(index, new JValue(value));
            return this;
        }

        protected override JsonArray OnInsertDouble(int index, double value)
        {
            _jArray?.Insert(index, new JValue(value));
            return this;
        }

        protected override JsonArray OnInsertFloat(int index, float value)
        {
            _jArray?.Insert(index, new JValue(value));
            return this;
        }

        protected override JsonArray OnInsertInt(int index, int value)
        {
            _jArray?.Insert(index, new JValue(value));
            return this;
        }

        protected override JsonArray OnInsertLong(int index, long value)
        {
            _jArray?.Insert(index, new JValue(value));
            return this;
        }

        protected override JsonArray OnInsertString(int index, string value)
        {
            _jArray?.Insert(index, new JValue(value));
            return this;
        }

        protected override JsonArray OnInsertJsonArray(int index, JsonArray value)
        {
            if (value == null)
            {
                return this;
            }
            try
            {
                _jArray?.Insert(index, ((JsonArrayImpl)value).GetInnerInstance());
            }
            catch (Exception)
            {
                _logger.Error("Can not put JsonArray by index: " + index);
            }
            return this;
        }

        protected override JsonArray OnInsertJsonObject(int index, JsonObject value)
        {
            if (value == null)
            {
                return this;
            }
            try
            {
                _jArray?.Insert(index, ((JsonObjectImpl)value).GetInnerInstance());
            }
            catch (Exception)
            {
                _logger.Error("Can not put JsonObject by index: " + index);
            }
            return this;
        }

        protected override bool OnParseBool(int index, bool defaultValue)
        {
            var result = defaultValue;
            try
            {
                result = (bool)_jArray[index];
            }
            catch (Exception)
            {
                _logger.Error("Can not parse bool value by index: " + index);
            }
            return result;
        }

        protected override double OnParseDouble(int index, double defaultValue)
        {
            var result = defaultValue;
            try
            {
                result = (double)_jArray[index];
            }
            catch (Exception)
            {
                _logger.Error("Can not parse double value by index: " + index);
            }
            return result;
        }

        protected override float OnParseFloat(int index, float defaultValue)
        {
            var result = defaultValue;
            try
            {
                result = (float)_jArray[index];
            }
            catch (Exception)
            {
                _logger.Error("Can not parse float value by index: " + index);
            }
            return result;
        }

        protected override int OnParseInt(int index, int defaultValue)
        {
            var result = defaultValue;
            try
            {
                result = (int)_jArray[index];
            }
            catch (Exception)
            {
                _logger.Error("Can not parse int value by index: " + index);
            }
            return result;
        }

        protected override long OnParseLong(int index, long defaultValue)
        {
            var result = defaultValue;
            try
            {
                result = (long)_jArray[index];
            }
            catch (Exception)
            {
                _logger.Error("Can not parse long value by index: " + index);
            }
            return result;
        }

        protected override string OnParseString(int index, string defaultValue)
        {
            var result = defaultValue;
            try
            {
                result = (string)_jArray[index];
            }
            catch (Exception)
            {
                _logger.Error("Can not parse string value by index: " + index);
            }
            return result;
        }

        protected override JsonArray OnParseJsonArray(int index)
        {
            try
            {
                return new JsonArrayImpl((JArray)_jArray[index]);
            }
            catch (Exception)
            {
                _logger.Error("Can not parse JArray value by index: " + index);
            }
            return null;
        }

        protected override JsonObject OnParseJsonObject(int index)
        {
            try
            {
                return new JsonObjectImpl((JObject)_jArray[index]);
            }
            catch (Exception)
            {
                _logger.Error("Can not parse JObject value by index: " + index);
            }
            return null;
        }

        protected override int OnSize()
        {
            return _jArray?.Count ?? 0;
        }

        protected override string OnToPrettyString()
        {
            return _jArray?.ToString(Formatting.Indented) ?? "";
        }

        public override string ToString()
        {
            return _jArray?.ToString(Formatting.None) ?? "";
        }
    }
}
