using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HB.Framework.Common.Api
{
    public class JwtApiRequest : ApiRequest
    {
        public JwtApiRequest(string productType, string apiVersion, HttpMethod httpMethod, string resourceName, string? condition = null)
            : base(productType, apiVersion, httpMethod, resourceName, condition)
        {
        }

        public void SetJwt(string jwt)
        {
            SetHeader("Authorization", "Bearer " + jwt);
        }

        public override Task<ApiResponse<T>> GetResponseAsync<T>(HttpClient httpClient)
        {
            return base.GetResponseAsync<T>(httpClient);
        }
    }
}