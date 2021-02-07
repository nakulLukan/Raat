using Raat.Shared;
using Raat.Web.Contracts;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Raat.Web.Services
{
    public class HttpContext : IHttpContext
    {
        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add(RequestHeader.DisplayId, Store.MyDisplayId);
            return client;
        }

        public async Task<T> ProcessPostRequest<T>(string apiEndpoint, object data)
        {
            try
            {
                using (var httpClient = GetClient())
                {
                    string requestUri = SharedConstant.ApiBaseUrl + apiEndpoint;
                    var jsonData = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
                    var result = await httpClient.PostAsync(requestUri, jsonData);
                    result.EnsureSuccessStatusCode();
                    string stringContent = await result.Content.ReadAsStringAsync();
                    Console.WriteLine($"Response: {stringContent}");
                    var responseData = JsonSerializer.Deserialize<T>(stringContent, new JsonSerializerOptions(JsonSerializerDefaults.Web));
                    Console.WriteLine($"Deserialized Response: {stringContent}");
                    return responseData;
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error Message: {e.Message}");
                throw;
            }
        }

        public async Task<T> ProcessGetRequest<T>(string apiEndpoint)
        {
            try
            {
                using (var httpClient = GetClient())
                {
                    string requestUri = SharedConstant.ApiBaseUrl + apiEndpoint;
                    var result = await httpClient.GetAsync(requestUri);
                    result.EnsureSuccessStatusCode();
                    string stringContent = await result.Content.ReadAsStringAsync();
                    Console.WriteLine($"Response: {stringContent}");
                    var responseData = JsonSerializer.Deserialize<T>(stringContent, new JsonSerializerOptions(JsonSerializerDefaults.Web));
                    Console.WriteLine($"Deserialized Response: {stringContent}");
                    return responseData;
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error Message: {e.Message}");
                throw;
            }
        }
    }
}
