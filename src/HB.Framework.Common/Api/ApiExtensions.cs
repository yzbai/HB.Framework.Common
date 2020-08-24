using HB.Framework.Common.Api;
using HB.Framework.Common.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace HB.Framework.Client.Api
{
    public static class ApiExtensions
    {
        private static readonly string[] _jsonContentTypes = new string[] { "application/json", "application/problem+json" };

        public static async Task<ApiResponse<T>> GetResponseAsync<T>(this ApiRequest request, HttpClient httpClient) where T : class
        {
            try
            {
                using HttpRequestMessage requestMessage = request.ToHttpRequestMessage();

                using HttpResponseMessage responseMessage = await httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

                return await responseMessage.ToApiResponseAsync<T>().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex, ApiErrorCode.ApiUtilsError, $"ApiRequestUtils.GetResponse {request.GetResourceName()}");
            }
        }

        /// <exception cref="InvalidOperationException">Ignore.</exception>
        public static HttpRequestMessage ToHttpRequestMessage(this ApiRequest request)
        {
            HttpMethod httpMethod = request.GetHttpMethod();

            if (request.GetNeedHttpMethodOveride() && (httpMethod == HttpMethod.Put || httpMethod == HttpMethod.Delete))
            {
                request.SetHeader("X-HTTP-Method-Override", httpMethod.Method);
                httpMethod = HttpMethod.Post;
            }

            HttpRequestMessage httpRequest = new HttpRequestMessage(httpMethod, BuildUrl(request));

            if (request.GetHttpMethod() != HttpMethod.Get)
            {
                //httpRequest.Content = new FormUrlEncodedContent(request.GetParameters());
                //httpRequest.Content = new JsonContent()

                if (request is BufferedFileApiRequest bufferedRequest)
                {
                    MultipartFormDataContent content = new MultipartFormDataContent();

#pragma warning disable CA2000 // Dispose objects before losing scope // using HttpRequestMessage 会自动dispose他的content
                    ByteArrayContent byteArrayContent = new ByteArrayContent(bufferedRequest.GetBytes());
#pragma warning restore CA2000 // Dispose objects before losing scope

                    content.Add(byteArrayContent, bufferedRequest.GetBytesPropertyName(), bufferedRequest.GetFileName());

                    request.GetParameters().ForEach(kv =>
                    {
                        content.Add(new StringContent(kv.Value), kv.Key);
                    });

                    httpRequest.Content = content;
                }
                else
                {
                    //TODO: .net 5以后，使用JsonContent
                    httpRequest.Content = new StringContent(SerializeUtil.ToJson(request.GetParameters()), Encoding.UTF8, "application/json");
                }
            }

            request.GetHeaders().ForEach(kv => httpRequest.Headers.Add(kv.Key, kv.Value));

            return httpRequest;
        }

        private static string BuildUrl(ApiRequest request)
        {
            StringBuilder requestUrlBuilder = new StringBuilder();

            if (!request.GetApiVersion().IsNullOrEmpty())
            {
                requestUrlBuilder.Append(request.GetApiVersion());
            }

            if (!request.GetResourceName().IsNullOrEmpty())
            {
                requestUrlBuilder.Append('/');
                requestUrlBuilder.Append(request.GetResourceName());
            }

            if (!request.GetCondition().IsNullOrEmpty())
            {
                requestUrlBuilder.Append('/');
                requestUrlBuilder.Append(request.GetCondition());
            }

            if (request.GetHttpMethod() == HttpMethod.Get)
            {
                string query = request.GetParameters().ToHttpValueCollection().ToString();

                if (!query.IsNullOrEmpty())
                {
                    requestUrlBuilder.Append('?');
                    requestUrlBuilder.Append(query);
                }
            }

            return requestUrlBuilder.ToString();
        }

        /// <exception cref="System.Text.Json.JsonException">Ignore.</exception>
        public static async Task<ApiResponse<T>> ToApiResponseAsync<T>(this HttpResponseMessage httpResponse) where T : class
        {
            if (httpResponse.IsSuccessStatusCode)
            {
                T? data = await httpResponse.DeSerializeJsonAsync<T>().ConfigureAwait(false);
                return new ApiResponse<T>(data, (int)httpResponse.StatusCode);
            }

            ApiError? apiError = await httpResponse.DeSerializeJsonAsync<ApiError>().ConfigureAwait(false);

            if (apiError == null)
            {
                return new ApiResponse<T>((int)httpResponse.StatusCode, Resources.InternalServerErrorMessage, ApiErrorCode.ApiUtilsError);
            }

            return new ApiResponse<T>((int)httpResponse.StatusCode, apiError.Message, apiError.Code);
        }

        public static async Task<T?> DeSerializeJsonAsync<T>(this HttpResponseMessage responseMessage) where T : class
        {
            if (typeof(T) == typeof(object))
            {
                return null;
            }

            string? mediaType = responseMessage.Content.Headers.ContentType?.MediaType;

            if (!_jsonContentTypes.Contains(mediaType))
            {
                return null;
            }

            Stream responseStream = await responseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false);

            T? data = await SerializeUtil.FromJsonAsync<T>(responseStream).ConfigureAwait(false);

            return data;
        }
    }
}
