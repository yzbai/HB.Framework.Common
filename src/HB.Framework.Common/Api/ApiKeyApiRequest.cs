using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HB.Framework.Common.Api
{
    public class ApiKeyApiRequest : ApiRequest
    {
        private readonly string _apiKeyName;

        public ApiKeyApiRequest(string apiKeyName, string productType, string apiVersion, HttpMethod httpMethod, string resourceName, string? condition = null)
            : base(productType, apiVersion, httpMethod, resourceName, condition)
        {
            _apiKeyName = apiKeyName;
        }

        [DisallowNull]
        public string? ApiKey
        {
            get
            {
                return GetHeader("X-Api-Key");
            }
            set
            {
                SetHeader("X-Api-Key", value);
            }
        }

        public override Task<ApiResponse<T>> GetResponseAsync<T>(HttpClient httpClient)
        {
            return base.GetResponseAsync<T>(httpClient);
        }

        public string GetApiKeyName()
        {
            return _apiKeyName;
        }
    }
}
