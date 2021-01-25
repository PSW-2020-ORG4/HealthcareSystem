using System;
using Backend.Model.Pharmacies;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
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
            var response = await SendRequest(request);
            if (!response.IsSuccessStatusCode)
                NotSuccessStatusCodeHandler(response.StatusCode);

            return JsonConvert.DeserializeObject<PharmacySystem>(await response.Content.ReadAsStringAsync());
        }
        public async Task<List<PharmacySystem>> GetAll()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "pharmacysystemservice/get/all");
            var response = await SendRequest(request);
            if (!response.IsSuccessStatusCode)
                NotSuccessStatusCodeHandler(response.StatusCode);

            return JsonConvert.DeserializeObject<List<PharmacySystem>>(await response.Content.ReadAsStringAsync());
        }
        public async Task<List<PharmacySystem>> GetBySubscribed(bool subscribed)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "pharmacysystemservice/get/subscribed");
            request.Content = new StringContent(subscribed.ToString(), Encoding.UTF8, "application/json");
            var response = await SendRequest(request);
            if (!response.IsSuccessStatusCode)
                NotSuccessStatusCodeHandler(response.StatusCode);

            return JsonConvert.DeserializeObject<List<PharmacySystem>>(await response.Content.ReadAsStringAsync());
        }
        public async Task<PharmacySystem> Create(PharmacySystem pharmacySystem)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "pharmacysystemservice");
            request.Content = new StringContent(JsonConvert.SerializeObject(pharmacySystem), Encoding.UTF8, "application/json");
            var response = await SendRequest(request);
            if (!response.IsSuccessStatusCode)
                NotSuccessStatusCodeHandler(response.StatusCode);

            return JsonConvert.DeserializeObject<PharmacySystem>(await response.Content.ReadAsStringAsync());
        }
        public async Task<bool> Update(PharmacySystem pharmacySystem)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, "pharmacysystemservice");
            request.Content = new StringContent(JsonConvert.SerializeObject(pharmacySystem), Encoding.UTF8, "application/json");
            var response = await SendRequest(request);
            if (!response.IsSuccessStatusCode)
                NotSuccessStatusCodeHandler(response.StatusCode);

            return true;
        }

        private void NotSuccessStatusCodeHandler(HttpStatusCode statusCode)
        {
            if ((int)statusCode >= 400 && (int)statusCode < 500)
                throw new Exception("Badly configured request to ActionBenefit service. Contact programmers!");
            if ((int)statusCode >= 500 && (int)statusCode < 600)
                throw new Exception("Something went wrong in ActionBenefit service. Contact programmers!");
        }

        private async Task<HttpResponseMessage> SendRequest(HttpRequestMessage request)
        {
            try
            {
                return await _httpClient.SendAsync(request);
            }
            catch
            {
                throw new Exception("PharmacySystem service could not be reached. Contact admin!");
            }
        }
    }
}
