using IntegrationAdapters.Apis.Grpc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IntegrationAdapters.Dtos;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Http;
using System;

namespace IntegrationAdapters.Apis.Http
{
    public class PharmacySystemApi_Id1
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl;

        public PharmacySystemApi_Id1(string baseUrl, HttpClient httpClient)
        {
            _client = httpClient;
            _baseUrl = baseUrl;
        }

        public async Task<List<Drug>> SearchDrugs(string apiKey, string search)
        {
            List<Drug> ret = new List<Drug>();
            var request = new HttpRequestMessage(HttpMethod.Get, _baseUrl+"/api/drugs/search");
            FindDrugRequest findDrugRequest = new FindDrugRequest() { ApiKey = apiKey, Name = search };
            string jsonContent = JsonConvert.SerializeObject(findDrugRequest);
            request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                ret.AddRange(JsonConvert.DeserializeObject<List<Drug>>(jsonResponse));
            }
            
            return ret;
        }

        public async Task<bool> SendDrugConsumptionRepor(string apiKey, string filePath, string fileName)
        {
            var stream = File.OpenRead(filePath + "/" + fileName);
            var formData = new MultipartFormDataContent
            {
                { new StreamContent(stream), "file", fileName },
                { new StringContent(apiKey), "apiKey" }
            };
            var request = new HttpRequestMessage(HttpMethod.Post, _baseUrl+"/file/uploadFile");
            request.Content = formData;

            HttpResponseMessage response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }

        public async Task<List<DrugListDTO>> GetAllDrugs(string apiKey)
        {
            List<DrugListDTO> ret = new List<DrugListDTO>();
            var request = new HttpRequestMessage(HttpMethod.Get, _baseUrl + "/api/noAuth/drug/getAll");
            HttpResponseMessage response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                ret.AddRange(JsonConvert.DeserializeObject<List<DrugListDTO>>(jsonResponse));
            }

            return ret;
        }

        public async Task<bool> GetDrugSpecificationsHttp(string apiKey, int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _baseUrl + $"/api/noAuth/drug/multipartdata/getById/{id}");
            HttpResponseMessage response = await _client.SendAsync(request);
            if (!response.IsSuccessStatusCode) return false;
            
            string jsonResponse = await response.Content.ReadAsStringAsync();
            List<string> ret = JsonConvert.DeserializeObject<List<string>>(jsonResponse);
            using (System.IO.FileStream reader = System.IO.File.Create(ret[0]))
            {
                byte[] buffer = Convert.FromBase64String(ret[1]);
                reader.Write(buffer, 0, buffer.Length);
            }
            return true;
            
        }

        public async Task<string> GetDrugSpecificationsSftp(string apiKey, int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _baseUrl + $"/api/noAuth/sftp/uploadJsch/{id}");
            HttpResponseMessage response = await _client.SendAsync(request);
            if (!response.IsSuccessStatusCode) return "";

            return await response.Content.ReadAsStringAsync();
        }
    }
}
