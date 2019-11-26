﻿using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace HB.Framework.Common.Test
{
    //TODO: 什么时间做单元测试？
    public class MapKey
    {
        public string KeyName { get; set; }
        public string KeyDescription { get; set; }
    }

    public class MapValue
    {
        public string Value { get; set; }
        public string ValueDescription { get; set; }
    }

    public class MapMapValue
    {
        public string Value { get; set; }
        public string ValueDescription { get; set; }

        public Dictionary<string, KeyValuePair<int, string>> MapValue { get; } = new Dictionary<string, KeyValuePair<int, string>>();
    }

    public class JsonStringTest
    {
        private readonly ITestOutputHelper _output;

        public JsonStringTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void TestJsonMapString()
        {
            Dictionary<MapKey, MapValue> map = new Dictionary<MapKey, MapValue>
            {
                { new MapKey { KeyName = "key1", KeyDescription = "key1__" }, new MapValue { Value = "value1", ValueDescription = "value1__" } },
                { new MapKey { KeyName = "key2", KeyDescription = "key2__" }, new MapValue { Value = "value2", ValueDescription = "value2__" } },
                { new MapKey { KeyName = "key3", KeyDescription = "key3__" }, new MapValue { Value = "value3", ValueDescription = "value3__" } },
                { new MapKey { KeyName = "key4", KeyDescription = "key4__" }, new MapValue { Value = "value4", ValueDescription = "value4__" } },
                { new MapKey { KeyName = "key5", KeyDescription = "key5__" }, new MapValue { Value = "value5", ValueDescription = "value5__" } }
            };

            string json = SerializeUtil.ToJson(map);

            _output.WriteLine(json);

            Assert.True(true);
        }

        [Fact]
        public void TestJsonMapString2()
        {
            Dictionary<string, MapValue> map = new Dictionary<string, MapValue>
            {
                { "key1", new MapValue { Value = "value1", ValueDescription = "value1__" } },
                { "key2", new MapValue { Value = "value2", ValueDescription = "value2__" } },
                { "key3", new MapValue { Value = "value3", ValueDescription = "value3__" } },
                { "key4", new MapValue { Value = "value4", ValueDescription = "value4__" } },
                { "key5", new MapValue { Value = "value5", ValueDescription = "value5__" } }
            };

            string json = SerializeUtil.ToJson(map);

            _output.WriteLine(json);

            Assert.True(true);
        }

        [Fact]
        public void TestJsonMapString3()
        {
            Dictionary<string, MapMapValue> map = new Dictionary<string, MapMapValue>();

            var mapValue1 = new Dictionary<string, KeyValuePair<int, string>>
            {
                { "mm1", new KeyValuePair<int, string>(1, "mm1__") },
                { "mm2", new KeyValuePair<int, string>(2, "mm2__") }
            };

            map.Add("key1", new MapMapValue { Value = "value1", ValueDescription = "value1__"});
            map["key1"].MapValue.Add(mapValue1);

            map.Add("key2", new MapMapValue { Value = "value2", ValueDescription = "value2__"});
            map["key2"].MapValue.Add(mapValue1);

            map.Add("key3", new MapMapValue { Value = "value3", ValueDescription = "value3__"});
            map["key3"].MapValue.Add(mapValue1);

            map.Add("key4", new MapMapValue { Value = "value4", ValueDescription = "value4__"});
            map["key4"].MapValue.Add(mapValue1);

            map.Add("key5", new MapMapValue { Value = "value5", ValueDescription = "value5__"});
            map["key5"].MapValue.Add(mapValue1);

            string json = SerializeUtil.ToJson(map);

            _output.WriteLine(json);

            Assert.True(true);
        }

        [Fact]
        public void TestJsonMapString4()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>
            {
                { "key1", "value1" },
                { "key2", "value2" }
            };

            ApplicationOptions optoins = new ApplicationOptions
            {
                Database = new DatabaseOptions() { Name = "presentfish.db" }
            };
            optoins.WebApis.Add("Authorization", new WebApiOptions() {

                BaseUrl = new Uri("http://192.168.0.112/")
            });

            optoins.WebApis["Authorization"].EndPoints.Add(dict);

            optoins.WebApis.Add("Common", new WebApiOptions()
            {

                BaseUrl = new Uri("http://192.168.0.112/")

            });

            optoins.WebApis["Common"].EndPoints.Add(dict);

            string jsonString = SerializeUtil.ToJson(optoins);

            _output.WriteLine(jsonString);

            Assert.True(true);
        }
    }

    public class DatabaseOptions
    {
        public string Name { get; set; }
    }

    public class WebApiOptions
    {
        public Uri BaseUrl { get; set; }

        public IDictionary<string, string> EndPoints { get; } = new Dictionary<string, string>();
    }

    public class ApplicationOptions
    {
        

        public DatabaseOptions Database { get; set; }

        public IDictionary<string, WebApiOptions> WebApis { get; } = new Dictionary<string, WebApiOptions>();
    }
}

//{
//    "HB.Framework.Common.Test.MapKey":{"Value":"value1","ValueDescription":"value1__"},
//    "HB.Framework.Common.Test.MapKey":{"Value":"value2","ValueDescription":"value2__"},
//    "HB.Framework.Common.Test.MapKey":{"Value":"value3","ValueDescription":"value3__"},
//    "HB.Framework.Common.Test.MapKey":{"Value":"value4","ValueDescription":"value4__"},
//    "HB.Framework.Common.Test.MapKey":{"Value":"value5","ValueDescription":"value5__"}
//}

//{
//    "key1":{"Value":"value1","ValueDescription":"value1__"},
//    "key2":{"Value":"value2","ValueDescription":"value2__"},
//    "key3":{"Value":"value3","ValueDescription":"value3__"},
//    "key4":{"Value":"value4","ValueDescription":"value4__"},
//    "key5":{"Value":"value5","ValueDescription":"value5__"}
//}


//{
//    "key1":{
//            "MapValue":{
//                "mm1":{"Key":1,"Value":"mm1__"},
//                "mm2":{"Key":2,"Value":"mm2__"}
//            },
//            "Value":"value1",
//            "ValueDescription":"value1__"
//    },
//    "key2":{
//            "MapValue":{
//                "mm1":{"Key":1,"Value":"mm1__"},
//                "mm2":{"Key":2,"Value":"mm2__"}
//            },
//            "Value":"value2",
//            "ValueDescription":"value2__"
//    },
//    "key3":{
//            "MapValue":{
//                "mm1":{"Key":1,"Value":"mm1__"},
//                "mm2":{"Key":2,"Value":"mm2__"}
//            },
//            "Value":"value3",
//            "ValueDescription":"value3__"
//    },
//    "key4":{
//            "MapValue":{
//                "mm1":{"Key":1,"Value":"mm1__"},
//                "mm2":{"Key":2,"Value":"mm2__"}
//            },
//            "Value":"value4",
//            "ValueDescription":"value4__"
//    },
//    "key5":{
//            "MapValue":{
//                "mm1":{"Key":1,"Value":"mm1__"},
//                "mm2":{"Key":2,"Value":"mm2__"}
//            },
//            "Value":"value5",
//            "ValueDescription":"value5__"
//    }
//}