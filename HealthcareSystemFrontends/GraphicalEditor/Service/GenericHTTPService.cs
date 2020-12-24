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
       
        public List<T> HTTPGetRequestWithObjectAsParam<T>(string requestURL, object objectParam)
        {
            var client = GetClient();
            var request = new RestRequest("api/" + requestURL, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(objectParam);
            var response = client.Execute<List<T>>(request);
            return response.Data;
        }

        public IRestResponse AddHTTPPostRequest(String requestURL, object objectToPost)
        {
            var client = GetClient();
            var request = new RestRequest("api/" + requestURL, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(objectToPost);
            IRestResponse response = client.Post(request);
            return response;
        }

        public T HTTPGetSingleItemRequest<T>(string requestURL)
        {
            var client = GetClient();
            var request = new RestRequest("api/" + requestURL, Method.GET);
            var response = client.Get<T>(request);
            return response.Data;
        }

        public List<T> HTTPGetRequest<T>(string requestURL)
        {
            var client = GetClient();
            var request = new RestRequest("api/" + requestURL,Method.GET);
            var response = client.Get<List<T>>(request);
            return response.Data;
        }

       
       


    }
}
