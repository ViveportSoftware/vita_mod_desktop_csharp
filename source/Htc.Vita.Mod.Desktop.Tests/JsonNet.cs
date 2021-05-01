using System.Collections.Generic;
using Htc.Vita.Core.Json;
using Htc.Vita.Core.Log;
using Htc.Vita.Mod.Desktop.JsonNet;
using Xunit;
using Xunit.Abstractions;

namespace Htc.Vita.Mod.Desktop.Tests
{
    public class JsonNet
    {
        private readonly ITestOutputHelper _output;

        public JsonNet(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public static void Default_0_GetInstance()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
        }

        [Fact]
        public static void Default_1_CreateJsonArray()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray);
            Assert.Equal("[]", jsonArray.ToString());
        }

        [Fact]
        public static void Default_2_CreateJsonObject()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonObject = jsonFactory.CreateJsonObject();
            Assert.NotNull(jsonObject);
            Assert.Equal("{}", jsonObject.ToString());
        }

        [Fact]
        public static void Default_3_DeserializeObject()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var content = "{\"TestBool1\":true,\"TestInt1\":3,\"TestString1\":\"test\"}";
            var testClass1 = jsonFactory.DeserializeObject<TestClass1>(content);
            Assert.NotNull(testClass1);
            Assert.True(testClass1.TestBool1);
            Assert.Equal(3, testClass1.TestInt1);
            Assert.Equal("test", testClass1.TestString1);
        }

        [Fact]
        public static void Default_3_DeserializeObject_WithBoolAndInt()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var content = "{\"TestBool1\":true,\"TestInt1\":3}";
            var testClass1 = jsonFactory.DeserializeObject<TestClass1>(content);
            Assert.NotNull(testClass1);
            Assert.True(testClass1.TestBool1);
            Assert.Equal(3, testClass1.TestInt1);
            Assert.Null(testClass1.TestString1);
        }

        [Fact]
        public static void Default_3_DeserializeObject_WithEmpty()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var content = "{}";
            var testClass1 = jsonFactory.DeserializeObject<TestClass1>(content);
            Assert.NotNull(testClass1);
            Assert.False(testClass1.TestBool1);
            Assert.Equal(0, testClass1.TestInt1);
            Assert.Null(testClass1.TestString1);
        }

        [Fact]
        public static void Default_3_DeserializeObject_WithNull()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var content = "";
            var testClass1 = jsonFactory.DeserializeObject<TestClass1>(content);
            Assert.Null(testClass1);
            testClass1 = jsonFactory.DeserializeObject<TestClass1>(null);
            Assert.Null(testClass1);
        }

        [Fact]
        public static void Default_3_DeserializeObject_AsList()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var content = "[{\"TestBool1\":true,\"TestInt1\":3,\"TestString1\":\"test\"},{\"TestBool1\":false,\"TestInt1\":5,\"TestString1\":null}]";
            var classList = jsonFactory.DeserializeObject<List<TestClass1>>(content);
            Assert.NotNull(classList);
            Assert.True(classList.Count == 2);
            var testClass0 = classList[0];
            Assert.NotNull(testClass0);
            Assert.True(testClass0.TestBool1);
            Assert.Equal(3, testClass0.TestInt1);
            Assert.Equal("test", testClass0.TestString1);
            var testClass1 = classList[1];
            Assert.NotNull(testClass1);
            Assert.False(testClass1.TestBool1);
            Assert.Equal(5, testClass1.TestInt1);
            Assert.Null(testClass1.TestString1);
        }

        [Fact]
        public static void Default_3_DeserializeObject_AsDictionary()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var content = "{\"testKey0\":\"testValue0\",\"testKey1\":\"testValue1\",\"testKey2\":\"testValue2\"}";
            var dict = jsonFactory.DeserializeObject<Dictionary<string, string>>(content);
            Assert.NotNull(dict);
            Assert.Equal("testValue0", dict["testKey0"]);
            Assert.Equal("testValue1", dict["testKey1"]);
            Assert.Equal("testValue2", dict["testKey2"]);
        }

        [Fact]
        public static void Default_3_DeserializeObject_AsListOfDictionary()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var content = "[{\"testKey0\":\"testValue0\",\"testKey1\":\"testValue1\",\"testKey2\":\"testValue2\"},{\"testKey0\":\"testValue3\",\"testKey2\":\"testValue4\",\"testKey4\":\"testValue5\"}]";
            var dictList = jsonFactory.DeserializeObject<List<Dictionary<string, string>>>(content);
            Assert.NotNull(dictList);
            Assert.True(dictList.Count == 2);
            var dict0 = dictList[0];
            Assert.NotNull(dict0);
            Assert.Equal("testValue0", dict0["testKey0"]);
            Assert.Equal("testValue1", dict0["testKey1"]);
            Assert.Equal("testValue2", dict0["testKey2"]);
            var dict1 = dictList[1];
            Assert.NotNull(dict1);
            Assert.Equal("testValue3", dict1["testKey0"]);
            Assert.Equal("testValue4", dict1["testKey2"]);
            Assert.Equal("testValue5", dict1["testKey4"]);
        }

        [Fact]
        public static void Default_4_GetJsonArray()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.GetJsonArray("[]");
            Assert.NotNull(jsonArray);
            Assert.Equal("[]", jsonArray.ToString());
        }

        [Fact]
        public static void Default_5_GetJsonObject()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonObject = jsonFactory.GetJsonObject("{}");
            Assert.NotNull(jsonObject);
            Assert.Equal("{}", jsonObject.ToString());
        }

        [Fact]
        public static void Default_6_SerializeObject()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var class1 = new TestClass1
            {
                TestBool1 = true,
                TestInt1 = 3,
                TestString1 = "test"
            };
            var result = jsonFactory.SerializeObject(class1);
            Assert.NotNull(result);
            Assert.Equal("{\"TestBool1\":true,\"TestInt1\":3,\"TestString1\":\"test\"}", result);
        }

        [Fact]
        public static void Default_6_SerializeObject_WithList()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var classList = new List<TestClass1>();
            var class1 = new TestClass1
            {
                TestBool1 = true,
                TestInt1 = 3,
                TestString1 = "test"
            };
            classList.Add(class1);
            var class2 = new TestClass1
            {
                TestInt1 = 5
            };
            classList.Add(class2);
            var result = jsonFactory.SerializeObject(classList);
            Assert.NotNull(result);
            Assert.Equal("[{\"TestBool1\":true,\"TestInt1\":3,\"TestString1\":\"test\"},{\"TestBool1\":false,\"TestInt1\":5,\"TestString1\":null}]", result);
        }

        [Fact]
        public static void Default_6_SerializeObject_WithDictionary()
        {
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var dict = new Dictionary<string, string>
            {
                {"testKey0", "testValue0"},
                {"testKey1", "testValue1"},
                {"testKey2", "testValue2"}
            };
            var result = jsonFactory.SerializeObject(dict);
            Assert.NotNull(result);
            Assert.Equal("{\"testKey0\":\"testValue0\",\"testKey1\":\"testValue1\",\"testKey2\":\"testValue2\"}", result);
        }

        [Fact]
        public void Default_6_SerializeObject_WithListOfDictionary()
        {
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var dict = new Dictionary<string, string>
            {
                {"testKey0", "testValue0"},
                {"testKey1", "testValue1"},
                {"testKey2", "testValue2"}
            };
            var dict2 = new Dictionary<string, string>
            {
                {"testKey0", "testValue3"},
                {"testKey2", "testValue4"},
                {"testKey4", "testValue5"}
            };
            var dictList = new List<Dictionary<string, string>>
            {
                dict,
                dict2
            };
            var result = jsonFactory.SerializeObject(dictList);
            _output.WriteLine("Serialized string: " + result);
            Assert.Equal("[{\"testKey0\":\"testValue0\",\"testKey1\":\"testValue1\",\"testKey2\":\"testValue2\"},{\"testKey0\":\"testValue3\",\"testKey2\":\"testValue4\",\"testKey4\":\"testValue5\"}]", result);
        }

        [Fact]
        public static void JsonArray_00_InsertBool()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray);
            Assert.Equal("[]", jsonArray.ToString());
            jsonArray.Insert(0, true);
            Assert.NotEqual("[]", jsonArray.ToString());
            Logger.GetInstance().Debug("jsonArray: " + jsonArray);
        }

        [Fact]
        public static void JsonArray_01_InsertDouble()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray);
            Assert.Equal("[]", jsonArray.ToString());
            jsonArray.Insert(0, 3.3D);
            Assert.NotEqual("[]", jsonArray.ToString());
            Logger.GetInstance().Debug("jsonArray: " + jsonArray);
        }

        [Fact]
        public static void JsonArray_02_InsertFloat()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray);
            Assert.Equal("[]", jsonArray.ToString());
            jsonArray.Insert(0, 2.2F);
            Assert.NotEqual("[]", jsonArray.ToString());
            Logger.GetInstance().Debug("jsonArray: " + jsonArray);
        }

        [Fact]
        public static void JsonArray_03_InsertInt()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray);
            Assert.Equal("[]", jsonArray.ToString());
            jsonArray.Insert(0, 1);
            Assert.NotEqual("[]", jsonArray.ToString());
            Logger.GetInstance().Debug("jsonArray: " + jsonArray);
        }

        [Fact]
        public static void JsonArray_04_InsertLong()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray);
            Assert.Equal("[]", jsonArray.ToString());
            jsonArray.Insert(0, 100000000000L);
            Assert.NotEqual("[]", jsonArray.ToString());
            Logger.GetInstance().Debug("jsonArray: " + jsonArray);
        }

        [Fact]
        public static void JsonArray_05_InsertString()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray);
            Assert.Equal("[]", jsonArray.ToString());
            jsonArray.Insert(0, "test");
            Assert.NotEqual("[]", jsonArray.ToString());
            Logger.GetInstance().Debug("jsonArray: " + jsonArray);
        }

        [Fact]
        public static void JsonArray_06_InsertJsonArray()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray);
            var jsonArray2 = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray2);
            Assert.Equal("[]", jsonArray.ToString());
            jsonArray.Insert(0, jsonArray2);
            Assert.NotEqual("[]", jsonArray.ToString());
            Logger.GetInstance().Debug("jsonArray: " + jsonArray);
        }

        [Fact]
        public static void JsonArray_07_InsertJsonObject()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray);
            var jsonObject = jsonFactory.CreateJsonObject();
            Assert.NotNull(jsonObject);
            Assert.Equal("[]", jsonArray.ToString());
            jsonArray.Insert(0, jsonObject);
            Assert.NotEqual("[]", jsonArray.ToString());
            Logger.GetInstance().Debug("jsonArray: " + jsonArray);
        }

        [Fact]
        public static void JsonArray_08_ParseBool()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray);
            Assert.Equal("[]", jsonArray.ToString());
            jsonArray.Insert(0, true);
            var value = jsonArray.ParseBool(0);
            Assert.True(value);
            jsonArray.Insert(1, "true");
            var value2 = jsonArray.ParseBool(1);
            Assert.True(value2);
        }

        [Fact]
        public static void JsonArray_09_ParseDouble()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray);
            Assert.Equal("[]", jsonArray.ToString());
            jsonArray.Insert(0, 3.3D);
            var value = jsonArray.ParseDouble(0);
            Assert.Equal(3.3D, value);
            jsonArray.Insert(1, "33.3");
            var value2 = jsonArray.ParseDouble(1);
            Assert.Equal(33.3D, value2);
        }

        [Fact]
        public static void JsonArray_10_ParseFloat()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray);
            Assert.Equal("[]", jsonArray.ToString());
            jsonArray.Insert(0, 2.2F);
            var value = jsonArray.ParseFloat(0);
            Assert.Equal(2.2F, value);
            jsonArray.Insert(1, "22.2");
            var value2 = jsonArray.ParseFloat(1);
            Assert.Equal(22.2F, value2);
        }

        [Fact]
        public static void JsonArray_11_ParseInt()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray);
            Assert.Equal("[]", jsonArray.ToString());
            jsonArray.Insert(0, 1);
            var value = jsonArray.ParseInt(0);
            Assert.Equal(1, value);
            jsonArray.Insert(1, "11");
            var value2 = jsonArray.ParseInt(1);
            Assert.Equal(11, value2);
        }

        [Fact]
        public static void JsonArray_12_ParseLong()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray);
            Assert.Equal("[]", jsonArray.ToString());
            jsonArray.Insert(0, 100000000000L);
            var value = jsonArray.ParseLong(0);
            Assert.Equal(100000000000L, value);
            jsonArray.Insert(1, "200000000000");
            var value2 = jsonArray.ParseLong(1);
            Assert.Equal(200000000000L, value2);
        }

        [Fact]
        public static void JsonArray_13_ParseString()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray);
            Assert.Equal("[]", jsonArray.ToString());
            jsonArray.Insert(0, "test");
            var value = jsonArray.ParseString(0);
            Assert.Equal("test", value);
        }

        [Fact]
        public static void JsonArray_14_ParseJsonArray()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray);
            var jsonArray2 = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray2);
            Assert.Equal("[]", jsonArray.ToString());
            jsonArray.Insert(0, jsonArray2);
            var value = jsonArray.ParseJsonArray(0);
            Assert.Equal("[]", value.ToString());
        }

        [Fact]
        public static void JsonArray_15_ParseJsonObject()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray);
            var jsonObject = jsonFactory.CreateJsonObject();
            Assert.NotNull(jsonObject);
            Assert.Equal("[]", jsonArray.ToString());
            jsonArray.Insert(0, jsonObject);
            var value = jsonArray.ParseJsonObject(0);
            Assert.Equal("{}", value.ToString());
        }

        [Fact]
        public static void JsonArray_16_Size()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray);
            Assert.Equal("[]", jsonArray.ToString());
            jsonArray.Insert(0, true);
            jsonArray.Insert(1, 3.3D);
            Logger.GetInstance().Debug("jsonArray: " + jsonArray);
            Assert.Equal(2, jsonArray.Size());
        }

        [Fact]
        public static void JsonObject_00_PutBool()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonObject = jsonFactory.CreateJsonObject();
            Assert.NotNull(jsonObject);
            Assert.Equal("{}", jsonObject.ToString());
            jsonObject.Put("key", true);
            Assert.NotEqual("{}", jsonObject.ToString());
            Logger.GetInstance().Debug("jsonObject: " + jsonObject);
        }

        [Fact]
        public static void JsonObject_01_PutDouble()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonObject = jsonFactory.CreateJsonObject();
            Assert.NotNull(jsonObject);
            Assert.Equal("{}", jsonObject.ToString());
            jsonObject.Put("key", 3.3D);
            Assert.NotEqual("{}", jsonObject.ToString());
            Logger.GetInstance().Debug("jsonObject: " + jsonObject);
        }

        [Fact]
        public static void JsonObject_02_PutFloat()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonObject = jsonFactory.CreateJsonObject();
            Assert.NotNull(jsonObject);
            Assert.Equal("{}", jsonObject.ToString());
            jsonObject.Put("key", 2.2F);
            Assert.NotEqual("{}", jsonObject.ToString());
            Logger.GetInstance().Debug("jsonObject: " + jsonObject);
        }

        [Fact]
        public static void JsonObject_03_PutInt()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonObject = jsonFactory.CreateJsonObject();
            Assert.NotNull(jsonObject);
            Assert.Equal("{}", jsonObject.ToString());
            jsonObject.Put("key", 1);
            Assert.NotEqual("{}", jsonObject.ToString());
            Logger.GetInstance().Debug("jsonObject: " + jsonObject);
        }

        [Fact]
        public static void JsonObject_04_PutLong()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonObject = jsonFactory.CreateJsonObject();
            Assert.NotNull(jsonObject);
            Assert.Equal("{}", jsonObject.ToString());
            jsonObject.Put("key", 100000000000L);
            Assert.NotEqual("{}", jsonObject.ToString());
            Logger.GetInstance().Debug("jsonObject: " + jsonObject);
        }

        [Fact]
        public static void JsonObject_05_PutString()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonObject = jsonFactory.CreateJsonObject();
            Assert.NotNull(jsonObject);
            Assert.Equal("{}", jsonObject.ToString());
            jsonObject.Put("key", "test");
            Assert.NotEqual("{}", jsonObject.ToString());
            Logger.GetInstance().Debug("jsonObject: " + jsonObject);
        }

        [Fact]
        public static void JsonObject_06_PutJsonArray()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonObject = jsonFactory.CreateJsonObject();
            Assert.NotNull(jsonObject);
            var jsonArray = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray);
            Assert.Equal("{}", jsonObject.ToString());
            jsonObject.Put("key", jsonArray);
            Assert.NotEqual("{}", jsonObject.ToString());
            Logger.GetInstance().Debug("jsonObject: " + jsonObject);
        }

        [Fact]
        public static void JsonObject_07_PutJsonObject()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonObject = jsonFactory.CreateJsonObject();
            Assert.NotNull(jsonObject);
            var jsonObject2 = jsonFactory.CreateJsonObject();
            Assert.NotNull(jsonObject2);
            Assert.Equal("{}", jsonObject.ToString());
            jsonObject.Put("key", jsonObject2);
            Assert.NotEqual("{}", jsonObject.ToString());
            Logger.GetInstance().Debug("jsonObject: " + jsonObject);
        }

        [Fact]
        public static void JsonObject_08_ParseBool()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonObject = jsonFactory.CreateJsonObject();
            Assert.NotNull(jsonObject);
            Assert.Equal("{}", jsonObject.ToString());
            jsonObject.Put("key", true);
            var value = jsonObject.ParseBool("key");
            Assert.True(value);
            jsonObject.Put("key2", "true");
            var value2 = jsonObject.ParseBool("key2");
            Assert.True(value2);
        }

        [Fact]
        public static void JsonObject_09_ParseDouble()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonObject = jsonFactory.CreateJsonObject();
            Assert.NotNull(jsonObject);
            Assert.Equal("{}", jsonObject.ToString());
            jsonObject.Put("key", 3.3D);
            var value = jsonObject.ParseDouble("key");
            Assert.Equal(3.3D, value);
            jsonObject.Put("key2", "33.3");
            var value2 = jsonObject.ParseDouble("key2");
            Assert.Equal(33.3D, value2);
        }

        [Fact]
        public static void JsonObject_10_ParseFloat()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonObject = jsonFactory.CreateJsonObject();
            Assert.NotNull(jsonObject);
            Assert.Equal("{}", jsonObject.ToString());
            jsonObject.Put("key", 2.2F);
            var value = jsonObject.ParseFloat("key");
            Assert.Equal(2.2F, value);
            jsonObject.Put("key2", "22.2");
            var value2 = jsonObject.ParseFloat("key2");
            Assert.Equal(22.2F, value2);
        }

        [Fact]
        public static void JsonObject_11_ParseInt()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonObject = jsonFactory.CreateJsonObject();
            Assert.NotNull(jsonObject);
            Assert.Equal("{}", jsonObject.ToString());
            jsonObject.Put("key", 1);
            var value = jsonObject.ParseInt("key");
            Assert.Equal(1, value);
            jsonObject.Put("key2", "11");
            var value2 = jsonObject.ParseInt("key2");
            Assert.Equal(11, value2);
        }

        [Fact]
        public static void JsonObject_12_ParseLong()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonObject = jsonFactory.CreateJsonObject();
            Assert.NotNull(jsonObject);
            Assert.Equal("{}", jsonObject.ToString());
            jsonObject.Put("key", 100000000000L);
            var value = jsonObject.ParseLong("key");
            Assert.Equal(100000000000L, value);
            jsonObject.Put("key2", "200000000000");
            var value2 = jsonObject.ParseLong("key2");
            Assert.Equal(200000000000L, value2);
        }

        [Fact]
        public static void JsonObject_13_ParseString()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonObject = jsonFactory.CreateJsonObject();
            Assert.NotNull(jsonObject);
            Assert.Equal("{}", jsonObject.ToString());
            jsonObject.Put("key", "test");
            var value = jsonObject.ParseString("key");
            Assert.Equal("test", value);
        }

        [Fact]
        public static void JsonObject_14_ParseJsonArray()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonObject = jsonFactory.CreateJsonObject();
            Assert.NotNull(jsonObject);
            var jsonArray = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray);
            Assert.Equal("{}", jsonObject.ToString());
            jsonObject.Put("key", jsonArray);
            var value = jsonObject.ParseJsonArray("key");
            Assert.Equal("[]", value.ToString());
        }

        [Fact]
        public static void JsonObject_15_ParseJsonObject()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonObject = jsonFactory.CreateJsonObject();
            Assert.NotNull(jsonObject);
            var jsonObject2 = jsonFactory.CreateJsonObject();
            Assert.NotNull(jsonObject2);
            Assert.Equal("{}", jsonObject.ToString());
            jsonObject.Put("key", jsonObject2);
            var value = jsonObject.ParseJsonObject("key");
            Assert.Equal("{}", value.ToString());
        }

        [Fact]
        public static void JsonObject_16_HasKey()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonObject = jsonFactory.CreateJsonObject();
            Assert.NotNull(jsonObject);
            Assert.Equal("{}", jsonObject.ToString());
            jsonObject.Put("key", true);
            Logger.GetInstance().Debug("jsonObject: " + jsonObject);
            Assert.True(jsonObject.HasKey("key"));
            Assert.False(jsonObject.HasKey("key2"));
        }

        [Fact]
        public static void JsonObject_17_AllKeys()
        {
            JsonFactory.Register<JsonFactoryImpl>();
            var jsonFactory = JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonObject = jsonFactory.CreateJsonObject();
            Assert.NotNull(jsonObject);
            Assert.Equal("{}", jsonObject.ToString());
            jsonObject.Put("key1", 1);
            jsonObject.Put("key2", true);
            jsonObject.Put("key3", 3.3D);
            Assert.Equal(3, jsonObject.AllKeys().Count);
        }

        public class TestClass1
        {
            public bool TestBool1 { get; set; }
            public int TestInt1 { get; set; }
            public string TestString1 { get; set; }
        }
    }
}
