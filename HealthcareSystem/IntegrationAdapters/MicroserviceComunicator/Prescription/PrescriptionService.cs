using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using IntegrationAdapters.Settings;
using Microsoft.Extensions.Options;
using IntegrationAdapters.Dtos;

namespace IntegrationAdapters.MicroserviceComunicator
{
    public class PrescriptionService : IPrescriptionService
    {
        private HttpClient _httpClient;

        public PrescriptionService(IHttpClientFactory httpClientFactory, IOptions<ServiceSettings> serviceSetting)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new System.Uri(serviceSetting.Value.PharmacySystemServiceUrl);
        }
        public async Task<List<PatientDto>> GetAllPatients()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "prescriptionservice/get/patients");
            var response = await SendRequest(request);
            if (!response.IsSuccessStatusCode)
                NotSuccessStatusCodeHandler(response.StatusCode);
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<PatientDto>>(jsonString);
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
                throw new Exception("Prescription service could not be reached. Contact admin!");
            }
        }
    }
}
