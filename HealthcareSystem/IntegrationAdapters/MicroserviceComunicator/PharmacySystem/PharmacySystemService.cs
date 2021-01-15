using Backend.Model.Pharmacies;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationAdapters.MicroserviceComunicator
{
    public class PharmacySystemService : IPharmacySystemService
    {
        private HttpClient _httpClient;

        public PharmacySystemService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new System.Uri("http://localhost:5001");
        }

        public async Task<PharmacySystem> Get(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "pharmacysystemservice/get");
            request.Content = new StringContent(id.ToString(), Encoding.UTF8, "application/json");
            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                return null;
            
            return JsonConvert.DeserializeObject<PharmacySystem>(await response.Content.ReadAsStringAsync());
        }
        public async Task<List<PharmacySystem>> GetAll()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "pharmacysystemservice/get/all");
            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                return null;

            return JsonConvert.DeserializeObject<List<PharmacySystem>>(await response.Content.ReadAsStringAsync());
        }
        public async Task<List<PharmacySystem>> GetBySubscribed(bool subscribed)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "pharmacysystemservice/get/subscribed");
            request.Content = new StringContent(subscribed.ToString(), Encoding.UTF8, "application/json");
            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                return null;

            return JsonConvert.DeserializeObject<List<PharmacySystem>>(await response.Content.ReadAsStringAsync());
        }
        public async Task<PharmacySystem> Create(PharmacySystem pharmacySystem)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "pharmacysystemservice");
            request.Content = new StringContent(JsonConvert.SerializeObject(pharmacySystem), Encoding.UTF8, "application/json");
            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                return null;

            return JsonConvert.DeserializeObject<PharmacySystem>(await response.Content.ReadAsStringAsync());
        }
        public async Task<bool> Update(PharmacySystem pharmacySystem)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, "pharmacysystemservice");
            request.Content = new StringContent(JsonConvert.SerializeObject(pharmacySystem), Encoding.UTF8, "application/json");
            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                return false;

            return true;
        }
    }
}
