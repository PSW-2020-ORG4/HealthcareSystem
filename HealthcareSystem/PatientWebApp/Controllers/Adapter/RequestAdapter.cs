using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace PatientWebApp.Controllers.Adapter
{
    public class RequestAdapter
    {
        public static ContentResult SendGetRequest(string baseUrl, string resource)
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest(resource);
            var response = client.Execute(request);

            var contentResult = new ContentResult();
            contentResult.Content = response.Content;
            contentResult.ContentType = "application/json";
            contentResult.StatusCode = (int)response.StatusCode;

            return contentResult;
        }
    }
}
