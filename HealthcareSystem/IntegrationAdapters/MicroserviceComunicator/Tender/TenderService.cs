using Backend.Model.DTO;
using Backend.Model.Pharmacies;
using IntegrationAdapters.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationAdapters.MicroserviceComunicator
{
    public class TenderService : ITenderService
    {
        private HttpClient _httpClient;

        public TenderService(IHttpClientFactory httpClientFactory, IOptions<ServiceSettings> serviceSettings)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new System.Uri(serviceSettings.Value.TenderServiceUrl);
        }
        public async Task<Tender> GetTender(int tenderId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "tenderservice/tender/get");
            request.Content = new StringContent(tenderId.ToString(), Encoding.UTF8, "application/json");
            var response = await SendRequest(request);
            if (!response.IsSuccessStatusCode)
                NotSuccessStatusCodeHandler(response.StatusCode);

            return JsonConvert.DeserializeObject<Tender>(await response.Content.ReadAsStringAsync());
        }
        public async Task<Tender> GetTenderByMessage(int tenderId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "tenderservice/tender/get/message");
            request.Content = new StringContent(tenderId.ToString(), Encoding.UTF8, "application/json");
            var response = await SendRequest(request);
            if (!response.IsSuccessStatusCode)
                NotSuccessStatusCodeHandler(response.StatusCode);

            return JsonConvert.DeserializeObject<Tender>(await response.Content.ReadAsStringAsync());
        }
        public async Task<List<Tender>> GetAllTenders()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "tenderservice/tender/get/all");
            var response = await SendRequest(request);
            if (!response.IsSuccessStatusCode)
                NotSuccessStatusCodeHandler(response.StatusCode);

            return JsonConvert.DeserializeObject<List<Tender>>(await response.Content.ReadAsStringAsync());
        }
        public async Task<bool> CreateTender(Tender tender)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "tenderservice/tender/create");
            request.Content = new StringContent(JsonConvert.SerializeObject(tender), Encoding.UTF8, "application/json");
            var response = await SendRequest(request);
            if (!response.IsSuccessStatusCode)
                NotSuccessStatusCodeHandler(response.StatusCode);

            return true;
        }
        public async Task<bool> CloseTender(int tenderId)
        { 
            var request = new HttpRequestMessage(HttpMethod.Patch, "tenderservice/tender/close");
            request.Content = new StringContent(tenderId.ToString(), Encoding.UTF8, "application/json");
            var response = await SendRequest(request);
            if (!response.IsSuccessStatusCode)
                NotSuccessStatusCodeHandler(response.StatusCode);

            return true;
        }
        public async Task<List<TenderDrugDTO>> GetDrugsByTender(int tenderId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "tenderservice/drug/get");
            request.Content = new StringContent(tenderId.ToString(), Encoding.UTF8, "application/json");
            var response = await SendRequest(request);
            if (!response.IsSuccessStatusCode)
                NotSuccessStatusCodeHandler(response.StatusCode);

            return JsonConvert.DeserializeObject<List<TenderDrugDTO>>(await response.Content.ReadAsStringAsync());
        }
        public async Task<TenderMessage> GetMessage(int messageId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "tenderservice/message/get");
            request.Content = new StringContent(messageId.ToString(), Encoding.UTF8, "application/json");
            var response = await SendRequest(request);
            if (!response.IsSuccessStatusCode)
                NotSuccessStatusCodeHandler(response.StatusCode);

            return JsonConvert.DeserializeObject<TenderMessage>(await response.Content.ReadAsStringAsync());
        }
        public async Task<List<TenderMessage>> GetAllMessages(int tenderId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "tenderservice/message/get/all");
            request.Content = new StringContent(tenderId.ToString(), Encoding.UTF8, "application/json");
            var response = await SendRequest(request);
            if (!response.IsSuccessStatusCode)
                NotSuccessStatusCodeHandler(response.StatusCode);

            return JsonConvert.DeserializeObject<List<TenderMessage>>(await response.Content.ReadAsStringAsync());
        }
        public async Task<TenderMessage> GetAcceptedMessage(int tenderId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "tenderservice/message/get/accepted");
            request.Content = new StringContent(tenderId.ToString(), Encoding.UTF8, "application/json");
            var response = await SendRequest(request);
            if (!response.IsSuccessStatusCode)
                NotSuccessStatusCodeHandler(response.StatusCode);

            return JsonConvert.DeserializeObject<TenderMessage>(await response.Content.ReadAsStringAsync());
        }
        public async Task<bool> AcceptMessage(int messageId)
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, "tenderservice/message/accept");
            request.Content = new StringContent(messageId.ToString(), Encoding.UTF8, "application/json");
            var response = await SendRequest(request);
            if (!response.IsSuccessStatusCode)
                NotSuccessStatusCodeHandler(response.StatusCode);

            return true;
        }

        private void NotSuccessStatusCodeHandler(HttpStatusCode statusCode)
        {
            if ((int)statusCode >= 400 && (int)statusCode < 500)
                throw new Exception("Badly configured request to Tender service. Contact programers!");
            if ((int)statusCode >= 500 && (int)statusCode < 600)
                throw new Exception("Something went wrong in Tender service. Contact programers!");
        }
        private async Task<HttpResponseMessage> SendRequest(HttpRequestMessage request)
        {
            try
            {
                return await _httpClient.SendAsync(request);
            }
            catch
            {
                throw new Exception("Tender service could not be reached. Contact admin!");
            }
        }
    }
}
