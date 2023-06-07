using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;

namespace ECommerceFrontEnd.Utilities
{
    public class InvokeClass
    {
        public static string Post(string baseUrl, string requestUri, Object request)
        {
            var response = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                var jsonData = JsonConvert.SerializeObject(request);

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage httpResponse = client.PostAsync(requestUri, content).GetAwaiter().GetResult();

                if (httpResponse.IsSuccessStatusCode)
                {
                    response = httpResponse.Content.ReadAsStringAsync().Result;
                }
            }

            return response;
        }
    }
}
