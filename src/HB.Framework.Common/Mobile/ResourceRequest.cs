using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;

namespace HB.Framework.Common.Mobile
{
    public class ResourceRequest : ValidatableObject
    {
        

        //All use fields instead of Properties, for avoid mvc binding
        private readonly string _productType;
        private readonly string _apiVersion;
        private readonly HttpMethod _httpMethod;
        private readonly bool _needAuthenticate;
        private readonly string _resourceName;
        private readonly string _condition;
        private readonly IDictionary<string, string> _headers = new Dictionary<string, string>();
        private readonly IDictionary<string, string> _parameters = new Dictionary<string, string>();

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

        public string GetCondition()
        {
            return _condition;
        }

        [Required]
        public string DeviceId
        {
            get
            {
                return GetParameter(MobileInfoNames.DeviceId);
            }

            set
            {
                SetParameter(MobileInfoNames.DeviceId, value);
            }
        }

        protected string GetParameter(string name)
        {
            if (_parameters.TryGetValue(name, out string value))
            {
                return value;
            }

            return null;
        }

        public IDictionary<string, string> GetParameters()
        {
            return _parameters;
        }

        protected void SetParameter(string name, string value)
        {
            _parameters[name] = value;
        }

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

        public void AddHeader(string name, string value)
        {
            if (_headers.ContainsKey(name))
            {
                throw new ArgumentException($"Request Already has a header named {name}");
            }

            _headers.Add(name, value);
        }

        public ResourceRequest(string productType, string apiVersion, HttpMethod httpMethod, bool needAuthenticate, string resourceName, string condition = null)
        {
            _productType = productType.ThrowIfNullOrEmpty(nameof(productType));
            _apiVersion = apiVersion;
            _httpMethod = httpMethod.ThrowIfNull(nameof(httpMethod));
            _needAuthenticate = needAuthenticate;
            _resourceName = resourceName;
            _condition = condition;
        }
    }
}
