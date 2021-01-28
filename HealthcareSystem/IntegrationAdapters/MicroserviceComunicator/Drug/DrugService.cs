using Backend.Model.Pharmacies;
using IntegrationAdapters.Settings;
using Microsoft.Extensions.Options;
using Model.Manager;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationAdapters.MicroserviceComunicator
{
    public class DrugService : IDrugService
    {
        private HttpClient _httpClient;

        public DrugService(IHttpClientFactory httpClientFactory, IOptions<ServiceSettings> serviceSettings)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new System.Uri(serviceSettings.Value.DrugServiceUrl);
        }

        public async Task<bool> AddQuantity(string code, int quantity)
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, "drugservice/addquantity");
            request.Content = new StringContent(JsonConvert.SerializeObject(new AddQuantityRequest(code, quantity)),
                                                                            Encoding.UTF8,
                                                                            "application/json");
            var response = await SendRequest(request);
            if ((int)response.StatusCode == 404)
                return false;
            if (!response.IsSuccessStatusCode)
                NotSuccessStatusCodeHandler(response.StatusCode);

            return true;
        }

        public async Task<List<Drug>> GetAll()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "drugservice/get/all");
            var response = await SendRequest(request);
            if (!response.IsSuccessStatusCode)
                NotSuccessStatusCodeHandler(response.StatusCode);

            return JsonConvert.DeserializeObject<List<Drug>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<List<DrugConsumptionReport>> GetDrugConsuption(DateRange dateRange)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "drugservice/get/drugconsuption");
            request.Content = new StringContent(JsonConvert.SerializeObject(dateRange),
                                                                            Encoding.UTF8,
                                                                            "application/json");
            var response = await SendRequest(request);
            if (!response.IsSuccessStatusCode)
                NotSuccessStatusCodeHandler(response.StatusCode);

            return JsonConvert.DeserializeObject<List<DrugConsumptionReport>>(await response.Content.ReadAsStringAsync());
        }

        private void NotSuccessStatusCodeHandler(HttpStatusCode statusCode)
        {
            if ((int)statusCode >= 400 && (int)statusCode < 500)
                throw new Exception("Badly configured request to Drug service. Contact programmers!");
            if ((int)statusCode >= 500 && (int)statusCode < 600)
                throw new Exception("Something went wrong in Drug service. Contact programmers!");
        }

        private async Task<HttpResponseMessage> SendRequest(HttpRequestMessage request)
        {
            try
            {
                return await _httpClient.SendAsync(request);
            }
            catch
            {
                throw new Exception("Drug service could not be reached. Contact admin!");
            }
        }
    }
}
