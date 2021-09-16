using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RemitaBillAndBulkPayments.Services
{
    public interface IHttpClientHelper
    {
        Task<string> GetWithBody(string url, Dictionary<string, string> headers = null, object req = null);
        Task<string> PostwithBody(string url, Dictionary<string, string> headers = null, string requestBody = null);
    }

    public class HttpClientHelper : IHttpClientHelper
    {
        private readonly ILogger<HttpClientHelper> _logger;

        public HttpClientHelper(ILogger<HttpClientHelper> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Sending a Get request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<string> GetWithBody(string url, Dictionary<string, string> headers = null, object req = null)
        {           
            var responseBody = string.Empty;

            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri(url),
                    Method = HttpMethod.Get,
                };

                try
                {
                    if (headers != null)
                    {
                        request.Content = new ByteArrayContent(Encoding.UTF8.GetBytes(""));
                        request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                        foreach (var item in headers)
                        {
                            request.Content.Headers.Add(item.Key, item.Value);
                        }
                    }

                    var result = client.SendAsync(request).Result;

                    responseBody = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
                    _logger.LogInformation($"GetWithBody: Results from {url} is: {responseBody}");

                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"GetWithBody: An Error Occured When Calling {url}, {ex.ToString()}");
                    throw new Exception(ex.Message);
                }
            }

            return responseBody;
        }


        /// <summary>
        /// Custom Post Method
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <param name="req"></param>
        /// <param name="requestBody"></param>
        /// <returns></returns>
        public async Task<string> PostwithBody(string url, Dictionary<string, string> headers = null, string requestBody = null)
        {
            if (requestBody != null)
            {
                _logger.LogInformation($"URL:PostwithBody---> {url}");
                _logger.LogInformation($"RequestObj:PostwithBody---> {JsonConvert.SerializeObject(requestBody)}");
            }
            else
            {
                _logger.LogInformation($"URL:PostwithBody---> {url}");
            }

            var responseBody = string.Empty;
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(5);

                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri(url),
                    Method = HttpMethod.Post,
                };

                if (!string.IsNullOrEmpty(requestBody))
                {
                    request.Content = new StringContent(requestBody); 
                    request.Content.Headers.Clear();
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                }

                try
                {
                    if (headers != null)
                    {
                        foreach (var item in headers)
                        {
                            request.Headers.TryAddWithoutValidation(item.Key, item.Value);
                        }
                    }

                    var result = await client.SendAsync(request);

                    _logger.LogInformation($"Raw response received: {JsonConvert.SerializeObject(result)}");

                    return await result.Content.ReadAsStringAsync();

                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"PostWithBody: An Error Occured When Calling {url}, {ex.ToString()}");
                }
            }

            return responseBody;
        }
    }
}
