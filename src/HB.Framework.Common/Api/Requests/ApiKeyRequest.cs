using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HB.Framework.Common.Api
{
    public class ApiKeyRequest : ApiRequest
    {
        private readonly string _apiKeyName;

        public ApiKeyRequest(string apiKeyName, string productType, string apiVersion, HttpMethod httpMethod, string resourceName, string? condition = null)
            : base(productType, apiVersion, httpMethod, resourceName, condition)
        {
            _apiKeyName = apiKeyName;
        }

        public void SetApiKey(string apiKey)
        {
            SetHeader("X-Api-Key", apiKey);
        }

        public string GetApiKeyName()
        {
            return _apiKeyName;
        }
    }
}
