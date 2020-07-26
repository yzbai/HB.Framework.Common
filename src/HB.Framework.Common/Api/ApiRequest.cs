#nullable enable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;

namespace HB.Framework.Common.Api
{
    public class ApiRequest : ValidatableObject
    {
        //All use fields instead of Properties, for avoid mvc binding
        private readonly string _productType;
        private readonly string _apiVersion;
        private readonly HttpMethod _httpMethod;
        private readonly bool _needAuthenticate;
        private readonly string _resourceName;
        private readonly string? _condition;
        private readonly IDictionary<string, string> _headers = new Dictionary<string, string>();
        private readonly IDictionary<string, string> _parameters = new Dictionary<string, string>();

        [Required]
        public string DeviceId
        {
            get { return GetParameter(ClientNames.DeviceId)!; }
            set { SetParameter(ClientNames.DeviceId, value); }
        }

        [Required]
        public string DeviceType
        {
            get { return GetParameter(ClientNames.DeviceType)!; }
            set { SetParameter(ClientNames.DeviceType, value); }
        }

        [Required]
        public string DeviceVersion
        {
            get { return GetParameter(ClientNames.DeviceVersion)!; }
            set { SetParameter(ClientNames.DeviceVersion, value); }
        }

        //[Required]
        //public string DeviceAddress
        //{
        //    get { return GetParameter(ClientNames.DeviceAddress)!; }
        //    set { SetParameter(ClientNames.DeviceAddress, value); }
        //}

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

        public ApiRequest(string productType, string apiVersion, HttpMethod httpMethod, bool needAuthenticate, string resourceName, string? condition = null)
        {
            _productType = productType;
            _apiVersion = apiVersion;
            _httpMethod = httpMethod;
            _needAuthenticate = needAuthenticate;
            _resourceName = resourceName;
            _condition = condition;

            RandomStr = SecurityUtil.CreateRandomString(6);
            Timestamp = SecurityUtil.GetCurrentTimestamp().ToString(GlobalSettings.Culture);
        }

        public string GetProductType()
        {
            return _productType;
        }

        public string GetApiVersion()
        {
            return _apiVersion;
        }

        public HttpMethod GetHttpMethod()
        {
            return _httpMethod;
        }

        public bool GetNeedAuthenticate()
        {
            return _needAuthenticate;
        }

        public string GetResourceName()
        {
            return _resourceName;
        }

        public string? GetCondition()
        {
            return _condition;
        }

        public IDictionary<string, string> GetParameters()
        {
            return _parameters;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <exception cref="System.ArgumentException"></exception>
        public void AddParameter(string name, string value)
        {
            if (_parameters.ContainsKey(name))
            {
                throw new ArgumentException($"Request Already has a parameter named {name}");
            }

            _parameters.Add(name, value);
        }

        public IDictionary<string, string> GetHeaders()
        {
            return _headers;
        }

        /// <exception cref="System.ArgumentException"></exception>
        public void AddHeader(string name, string value)
        {
            if (_headers.ContainsKey(name))
            {
                throw new ArgumentException($"Request Already has a header named {name}");
            }

            _headers.Add(name, value);
        }

        protected string? GetParameter(string name)
        {
            if (_parameters.TryGetValue(name, out string value))
            {
                return value;
            }

            return null;
        }

        protected void SetParameter(string name, string value)
        {
            _parameters[name] = value;
        }
    }
}
