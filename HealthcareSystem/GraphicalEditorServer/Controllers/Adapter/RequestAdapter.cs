using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace GraphicalEditorServer.Controllers.Adapter
{
    public class RequestAdapter
    {
        public static ContentResult SendRequestWithoutBody(string baseUrl, string resource, Method method)
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest(resource, method);
            var response = client.Execute(request);

            var contentResult = new ContentResult();
            contentResult.Content = response.Content;
            contentResult.ContentType = "application/json";
            contentResult.StatusCode = (int)response.StatusCode;

            return contentResult;
        }

        public static ContentResult SendRequestWithBody<T>(string baseUrl, string resource, T dto)
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest(resource, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(dto);
            var response = client.Execute(request);

            var contentResult = new ContentResult();
            contentResult.Content = response.Content;
            contentResult.ContentType = "application/json";
            contentResult.StatusCode = (int)response.StatusCode;

            return contentResult;
        }
    }
}
