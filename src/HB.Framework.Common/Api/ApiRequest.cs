#nullable enable

using HB.Framework.Client.Api;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading.Tasks;

namespace HB.Framework.Common.Api
{
    public class ApiRequest : ValidatableObject
    {
        [Required]
        public string DeviceId
        {
            get { return GetParameter(ClientNames.DeviceId)!; }
            set { SetParameter(ClientNames.DeviceId, value); }
        }

        [Required]
        public DeviceInfos DeviceInfos
        {
            get { return DeviceInfos.FromString(GetParameter(ClientNames.DeviceInfos)!); }
            set { SetParameter(ClientNames.DeviceInfos, value.ToString()); }
        }

        [Required]
        public string DeviceVersion
        {
            get { return GetParameter(ClientNames.DeviceVersion)!; }
            set { SetParameter(ClientNames.DeviceVersion, value); }
        }

        [Required]
        public string RandomStr
        {
            get { return GetParameter(ClientNames.RandomStr)!; }
            set { SetParameter(ClientNames.RandomStr, value); }
        }

        [Required]
        public string Timestamp
        {
            get { return GetParameter(ClientNames.Timestamp)!; }
            set { SetParameter(ClientNames.Timestamp, value); }
        }

        public string? PublicResourceToken
        {
            get => GetParameter(ClientNames.PublicResourceToken);
            set => SetParameter(ClientNames.PublicResourceToken, value);
        }

        //All use fields & Get Methods instead of Properties, for avoid mvc binding
        private readonly string _productName;
        private readonly string _apiVersion;
        private readonly HttpMethod _httpMethod;
        private readonly string _resourceName;
        private readonly string? _condition;
        private bool _needHttpMethodOveride = true;
        private readonly IDictionary<string, string> _headers = new Dictionary<string, string>();
        private readonly IDictionary<string, string?> _parameters = new Dictionary<string, string?>();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="productName"></param>
        /// <param name="apiVersion"></param>
        /// <param name="httpMethod"></param>
        /// <param name="resourceName"></param>
        /// <param name="condition">同一Verb下的条件分支，比如在ApiController上标注的[HttpGet("BySms")],BySms就是condition</param>
        public ApiRequest(string productName, string apiVersion, HttpMethod httpMethod, string resourceName, string? condition = null)
        {
            _productName = productName;
            _apiVersion = apiVersion;
            _httpMethod = httpMethod;
            _resourceName = resourceName;
            _condition = condition;

            RandomStr = SecurityUtil.CreateRandomString(6);
            Timestamp = SecurityUtil.GetCurrentTimestamp().ToString(GlobalSettings.Culture);
        }

        public string GetProductName()
        {
            return _productName;
        }

        public string GetApiVersion()
        {
            return _apiVersion;
        }

        public HttpMethod GetHttpMethod()
        {
            return _httpMethod;
        }

        public string GetResourceName()
        {
            return _resourceName;
        }

        public string? GetCondition()
        {
            return _condition;
        }

        public bool GetNeedHttpMethodOveride()
        {
            return _needHttpMethodOveride;
        }
        public void SetNeedHttpMethodOveride(bool isNeeded)
        {
            _needHttpMethodOveride = isNeeded;
        }

        public string? GetHeader(string name)
        {
            if (_headers.TryGetValue(name, out string value))
            {
                return value;
            }

            return null;
        }

        /// <exception cref="System.ArgumentException"></exception>
        public void SetHeader(string name, string value)
        {
            _headers[name] = value;
        }

        public IDictionary<string, string> GetHeaders()
        {
            return _headers;
        }

        public string? GetParameter(string name)
        {
            if (_parameters.TryGetValue(name, out string? value))
            {
                return value;
            }

            return null;
        }

        public void SetParameter(string name, string? value)
        {
            ThrowIf.NullOrEmpty(name, nameof(name));

            _parameters[name] = value;
        }

        public IDictionary<string, string?> GetParameters()
        {
            return _parameters;
        }
    }
}
