using Backend.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationAdapters.MicroserviceComunicator
{
    public class ActionBenefitService : IActionBenefitService
    {
        private HttpClient _httpClient;

        public ActionBenefitService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new System.Uri("http://localhost:5002");
        }
        public async Task<ActionBenefit> Get(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "actionbenefitservice/get");
            request.Content = new StringContent(id.ToString(), Encoding.UTF8, "application/json");
            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                return null;

            return JsonConvert.DeserializeObject<ActionBenefit>(await response.Content.ReadAsStringAsync());
        }

        public async Task<List<ActionBenefit>> GetAll()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "actionbenefitservice/get/all");
            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                return null;

            return JsonConvert.DeserializeObject<List<ActionBenefit>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<bool> SetPublic(int id, bool isPublic)
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, "actionbenefitservice/setpublic");
            request.Content = new StringContent(JsonConvert.SerializeObject(new SetPublicRequest(id, isPublic)), 
                                                Encoding.UTF8, 
                                                "application/json");
            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                return false;

            return true;
        }
    }
}
