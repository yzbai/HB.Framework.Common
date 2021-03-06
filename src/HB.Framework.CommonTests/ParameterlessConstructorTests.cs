﻿using HB.Framework.CommonTests.Mocker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace HB.Framework.CommonTests
{
    public class ParameterlessConstructorTests
    {
        /// <summary>
        /// System.Text.Json 不支持没有包含无参构造函数的类
        /// </summary>
        [Fact]
        public void Json_Parameterless_Test()
        {
            SimpleCls simpleCls = new SimpleCls("xx", "tt", 12);

            string json =  SerializeUtil.ToJson(simpleCls);

            SimpleCls? backSimpleCls = SerializeUtil.FromJson<SimpleCls>(json);
            

        }

        /// <summary>
        /// IConfiguration.Bind不支持没有包含无参构造函数的类
        /// </summary>
        [Fact]
        public void Configuration_Bind_Parameterless_Test()
        {
            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("app.json").Build();

            SimpleAppOptions simpleAppOptions = new SimpleAppOptions();

            configuration.GetSection("SimpleAppOptions").Bind(simpleAppOptions);
        }

        [Fact]
        public void Options_Parameterless_Test()
        {
            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("app.json").Build();

            ServiceCollection services = new ServiceCollection();

            services.Configure<SimpleAppOptions>(configuration.GetSection("SimpleAppOptions"));

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            IOptions<SimpleAppOptions>? options = serviceProvider.GetRequiredService<IOptions<SimpleAppOptions>>();

            SimpleAppOptions simpleAppOptions = options.Value;
        }
    }
}
