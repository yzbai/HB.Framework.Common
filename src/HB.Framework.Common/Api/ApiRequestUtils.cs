﻿using HB.Framework.Common.Api;
using HB.Framework.Common.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HB.Framework.Client.Api
{
    public static class ApiRequestUtils
    {
        public static async Task<ApiResponse<T>> GetResponse<T>(ApiRequest request, HttpClient httpClient, bool needHttpMethodOveride) where T : ApiResponseData
        {
            try
            {
                using HttpRequestMessage httpRequestMessage = ToHttpRequestMessage(request, needHttpMethodOveride);

                using HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

                return await ToApiResponseAsync<T>(httpResponseMessage).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new ApiErrorException(ex, ApiError.ApiInternalError, $"Request: {SerializeUtil.ToJson(request)}");
            }
        }

        /// <exception cref="InvalidOperationException">Ignore.</exception>
        private static HttpRequestMessage ToHttpRequestMessage(ApiRequest request, bool needHttpMethodOveride)
        {
            HttpMethod httpMethod = request.GetHttpMethod();

            if (needHttpMethodOveride && (httpMethod == HttpMethod.Put || httpMethod == HttpMethod.Delete))
            {
                request.SetHeader("X-HTTP-Method-Override", httpMethod.Method);
                httpMethod = HttpMethod.Post;
            }

            HttpRequestMessage httpRequest = new HttpRequestMessage(httpMethod, ToUrl(request));

            if (request.GetHttpMethod() != HttpMethod.Get)
            {
                httpRequest.Content = new FormUrlEncodedContent(request.GetParameters());
            }

            request.GetHeaders().ForEach(kv => httpRequest.Headers.Add(kv.Key, kv.Value));

            return httpRequest;
        }

#pragma warning disable CA1055 // Uri return values should not be strings
        private static string ToUrl(ApiRequest request)
#pragma warning restore CA1055 // Uri return values should not be strings
        {
            StringBuilder requestUrlBuilder = new StringBuilder();

            if (!request.GetApiVersion().IsNullOrEmpty())
            {
                requestUrlBuilder.Append(request.GetApiVersion());
            }

            if (!request.GetResourceName().IsNullOrEmpty())
            {
                requestUrlBuilder.Append("/");
                requestUrlBuilder.Append(request.GetResourceName());
            }

            if (!request.GetCondition().IsNullOrEmpty())
            {
                requestUrlBuilder.Append("/");
                requestUrlBuilder.Append(request.GetCondition());
            }

            if (request.GetHttpMethod() == HttpMethod.Get)
            {
                string query = request.GetParameters().ToHttpValueCollection().ToString();

                if (!query.IsNullOrEmpty())
                {
                    requestUrlBuilder.Append("?");
                    requestUrlBuilder.Append(query);
                }
            }

            return requestUrlBuilder.ToString();
        }

        /// <exception cref="System.Text.Json.JsonException">Ignore.</exception>
        private static async Task<ApiResponse<T>> ToApiResponseAsync<T>(HttpResponseMessage httpResponse) where T : ApiResponseData
        {
            string? mediaType = httpResponse.Content.Headers.ContentType?.MediaType;

            bool hasJsonData = typeof(T) != typeof(ApiResponseData);

            if (httpResponse.IsSuccessStatusCode)
            {
                if ("application/json".Equals(mediaType, StringComparison.CurrentCulture) && hasJsonData)
                {
                    Stream responseStream = await httpResponse.Content.ReadAsStreamAsync().ConfigureAwait(false);

                    T data = await SerializeUtil.FromJsonAsync<T>(responseStream).ConfigureAwait(false);

                    return new ApiResponse<T>(data, (int)httpResponse.StatusCode);
                }
                else
                {
                    return new ApiResponse<T>(null, (int)httpResponse.StatusCode);
                }
            }
            else
            {
                if (hasJsonData && ("application/problem+json".Equals(mediaType, StringComparison.CurrentCulture) || "application/json".Equals(mediaType, StringComparison.CurrentCulture)))
                {
                    Stream responseStream = await httpResponse.Content.ReadAsStreamAsync().ConfigureAwait(false);

                    ApiErrorResponse errorResponse = await SerializeUtil.FromJsonAsync<ApiErrorResponse>(responseStream).ConfigureAwait(false);

                    return new ApiResponse<T>((int)httpResponse.StatusCode, errorResponse.Message, errorResponse.Code);
                }
                else
                {
                    return new ApiResponse<T>((int)httpResponse.StatusCode, Resources.InternalServerErrorMessage, ApiError.ApiInternalError);
                }
            }
        }
    }
}
