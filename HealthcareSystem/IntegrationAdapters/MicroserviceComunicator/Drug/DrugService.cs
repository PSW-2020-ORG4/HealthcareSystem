using Backend.Model.Pharmacies;
using Model.Manager;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationAdapters.MicroserviceComunicator
{
    public class DrugService : IDrugService
    {
        private HttpClient _httpClient;

        public DrugService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new System.Uri("http://localhost:5003");
        }

        public async Task<bool> AddQuantity(string code, int quantity)
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, "drugservice/addquantity");
            request.Content = new StringContent(JsonConvert.SerializeObject(new AddQuantityRequest(code, quantity)), 
                                                                            Encoding.UTF8, 
                                                                            "application/json");
            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                return false;

            return true;
        }

        public async Task<List<Drug>> GetAll()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "drugservice/get/all");
            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                return null;

            return JsonConvert.DeserializeObject<List<Drug>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<List<DrugConsumptionReport>> GetDrugConsuption(DateRange dateRange)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "drugservice/get/drugconsuption");
            request.Content = new StringContent(JsonConvert.SerializeObject(dateRange),
                                                                            Encoding.UTF8,
                                                                            "application/json");
            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                return null;

            return JsonConvert.DeserializeObject<List<DrugConsumptionReport>>(await response.Content.ReadAsStringAsync());
        }
    }
}
