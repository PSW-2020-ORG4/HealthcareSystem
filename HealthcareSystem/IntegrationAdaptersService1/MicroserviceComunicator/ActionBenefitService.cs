using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationAdaptersService1.MicroserviceComunicator
{
    public class ActionBenefitService : IActionBenefitService
    {
        private HttpClient _httpClient;
        public ActionBenefitService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new System.Uri("http://localhost:5002");
        }

        public async Task<bool> Subscribe(string exchangeName)
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, "actionbenefitservice/subscribe");
            request.Content = new StringContent(exchangeName, Encoding.UTF8, "application/json");
            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                return false;

            return true;
        }

        public async Task<bool> SubscriptionEdit(string exOld, bool subOld, string exNew, bool subNew)
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, "actionbenefitservice/subscriptionedit");
            request.Content = new StringContent(
                    JsonConvert.SerializeObject(new SubEditRequest(exOld, subOld, exNew, subNew)),
                    Encoding.UTF8, 
                    "application/json");
            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                return false;

            return true;
        }
    }
}
