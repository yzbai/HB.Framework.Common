using Xunit;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;
using HB.Framework.CommonTests;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Buffers.Text;
using System.Buffers;

namespace System.Tests
{
    public class SerializeUtilTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public SerializeUtilTests(ITestOutputHelper testOutputHelper)
        {
            _outputHelper = testOutputHelper;
        }

        [Fact()]
        public void ToJsonTest()
        {
            var student = StudentMocker.MockOneStudent();

            string json = SerializeUtil.ToJson(student);
            string newtonJson = Newtonsoft.Json.JsonConvert.SerializeObject(student, new Newtonsoft.Json.Converters.StringEnumConverter());

            Assert.Equal(json, newtonJson);
        }

        [Fact()]
        public void ToJsonTest_ChineseSymbol()
        {
            object jsonObject = new { chinese_symbol = @"~·@#￥%……&*（）—-+=｛｝【】；：“”‘’《》，。？、" };
            string json = SerializeUtil.ToJson(jsonObject);
            string newtonJson = Newtonsoft.Json.JsonConvert.SerializeObject(value: jsonObject);

            Assert.Equal(expected: json, actual: newtonJson);
        }

        [Fact()]
        public void FromJsonTest_Number()
        {
            string json = "{\"Number\": \"123\", \"Price\": \"12.123456789\"}";
            

            NumberTestCls obj = SerializeUtil.FromJson<NumberTestCls>(json);

            NumberTestCls newtonObj = Newtonsoft.Json.JsonConvert.DeserializeObject<NumberTestCls>(json);

            Assert.True(obj.Number == newtonObj.Number && obj.Price == newtonObj.Price);
        }
    }

    
    class NumberTestCls
    {
        [System.Text.Json.Serialization.JsonConverter(typeof(IntToStringConverter))]
        public int Number { get; set; }

        [JsonConverter(typeof(DoubleToStringConverter))]
        public double Price { get; set; }
    }

   

}