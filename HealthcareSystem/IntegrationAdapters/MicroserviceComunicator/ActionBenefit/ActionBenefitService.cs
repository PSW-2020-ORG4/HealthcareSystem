using Backend.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
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
            var response = await SendRequest(request);
            if (!response.IsSuccessStatusCode)
                NotSuccessStatusCodeHandler(response.StatusCode);

            return JsonConvert.DeserializeObject<ActionBenefit>(await response.Content.ReadAsStringAsync());
        }

        public async Task<List<ActionBenefit>> GetAll()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "actionbenefitservice/get/all");
            var response = await SendRequest(request);
            if (!response.IsSuccessStatusCode)
                NotSuccessStatusCodeHandler(response.StatusCode);

            return JsonConvert.DeserializeObject<List<ActionBenefit>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<bool> SetPublic(int id, bool isPublic)
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, "actionbenefitservice/setpublic");
            request.Content = new StringContent(JsonConvert.SerializeObject(new SetPublicRequest(id, isPublic)),
                                                Encoding.UTF8,
                                                "application/json");
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
                throw new Exception("ActionBenefit service could not be reached. Contact admin!");
            }
        }
    }
}
