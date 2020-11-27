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
        private RestClient GetClient()
        {
            var client = new RestSharp.RestClient("http://localhost:" + ServerConstants.PORT);
            return client;
        }

        public void AddHTTPRequest(String requestURL, String JSONContent)
        {
            var client = GetClient();
            var request = new RestRequest("api/" + requestURL);
            request.AddJsonBody(JSONContent);
            IRestResponse response = client.Post(request);
        }
    }
}
