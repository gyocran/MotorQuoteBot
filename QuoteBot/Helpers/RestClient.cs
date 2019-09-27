using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace QuoteBot.Helpers
{
    public class RestClient<T>
    {
        private const string WebServiceUrl = "https://activainsurance-test.azurewebsites.net/";  //Activa Test Environment

        public static async Task<T> GetAsync(string _action)
        {
            var httpClient = new HttpClient();
            var _content = "";
            var optionsList = new List<string>();
            try
            {
                //var _bearer = "Bearer " + AppSettings.GetValueOrDefault("access_token", string.Empty);
                //httpClient.DefaultRequestHeaders.Add("Authorization", _bearer);

                var uri = new Uri(string.Format(WebServiceUrl + _action, string.Empty));
                var json = await httpClient.GetAsync(uri);

                if (json.IsSuccessStatusCode)
                {
                    _content = await json.Content.ReadAsStringAsync();
                    //result = JsonConvert.DeserializeObject<List<Lookups>>(_content);
                }

                //if (result != null)
                //{
                //    foreach (var option in result)
                //    {
                //        optionsList.Add(option.Description);
                //    }
                //}
            }
            catch (Exception ex)
            {
                //Crash reporting to App Center
                var properties = new Dictionary<string, string> {
                    { "Page", "RestClient" },
                    { "Method", "GetAsync" },
                    { "URL", _action }
                };

            }
            return JsonConvert.DeserializeObject<T>(_content);
        }

        public static async Task<T> PostAsync(object param, string _action)
        {
            var httpClient = new HttpClient();
            var _content = "";
            var optionsList = new List<string>();

            try
            {
                //var _bearer = "Bearer " + AppSettings.GetValueOrDefault("access_token", string.Empty);
                //httpClient.DefaultRequestHeaders.Add("Authorization", _bearer);
                var json = JsonConvert.SerializeObject(param);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Add("Cookie", "auth=ArbitrarySessionToken");

                var uri = new Uri(string.Format(WebServiceUrl + _action, string.Empty));
                var response = await httpClient.PostAsync(uri, httpContent);

                if (response.IsSuccessStatusCode)
                {
                    _content = await response.Content.ReadAsStringAsync();
                }

                //if (result != null)
                //{
                //    foreach (var option in result)
                //    {
                //        optionsList.Add(option.Description);
                //    }
                //}
            }
            catch (Exception ex)
            {
                //Crash reporting to App Center
                var properties = new Dictionary<string, string> {
                    { "Page", "RestClient" },
                    { "Method", "GetAsync" },
                    { "URL", _action }
                };

            }
            return JsonConvert.DeserializeObject<T>(_content);
        }
    }
}
