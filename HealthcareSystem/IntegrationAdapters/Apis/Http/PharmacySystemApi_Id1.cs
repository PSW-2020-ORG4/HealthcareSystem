using IntegrationAdapters.Apis.Grpc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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

    }
}
