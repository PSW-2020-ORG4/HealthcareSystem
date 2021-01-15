using Backend.Model.Users;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace IntegrationAdapters.MicroserviceComunicator
{
    public class PrescriptionService : IPrescriptionService
    {
        private HttpClient _httpClient;

        public PrescriptionService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new System.Uri("http://localhost:5001");
        }
        public async Task<List<Patient>> GetAllPatients()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "prescriptionservice/get/patients");
            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                return null;
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Patient>>(jsonString);
        }
    }
}
