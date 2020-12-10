using IntegrationAdapters.Protos;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationAdapters.APIs
{
    public class PharmacySystemId1RestApiClient
    {
        public HttpClient _client { get; private set; }
        private readonly string _baseUrl;

        public PharmacySystemId1RestApiClient(HttpClient client, string baseUrl)
        {
            _client = client;
            _baseUrl = baseUrl;
        }

        public async Task<List<Drug>> SearchDrugs(string apiKey, string search)
        {
            List<Drug> ret = new List<Drug>();
            var request = new HttpRequestMessage(HttpMethod.Get, _baseUrl+"/api/test");
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
