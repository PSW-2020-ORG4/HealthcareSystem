using GraphicalEditor.Constants;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.Service
{
    public class GenericHTTPService
    {
        protected RestClient GetClient()
        {
            var client = new RestSharp.RestClient("http://localhost:" + ServerConstants.PORT);
            return client;
        }

        public void AddHTTPPostRequest(String requestURL, String JSONContent)
        {
            var client = GetClient();
            var request = new RestRequest("api/" + requestURL);
            request.AddJsonBody(JSONContent);
            IRestResponse response = client.Post(request);
        }

        public List<T> HTTPGetRequest<T>(string requestURL)
        {
            var client = GetClient();
            var request = new RestRequest("api/" + requestURL,Method.GET);
            var response = client.Get<List<T>>(request);
            return response.Data;
        }

        public void AddHTTPGetRequest(String requestURL, String JSONContent)
        {
            var client = GetClient();
            var request = new RestRequest("api/" + requestURL);
            request.AddJsonBody(JSONContent);
            IRestResponse response = client.Post(request);
        }
    }
}
