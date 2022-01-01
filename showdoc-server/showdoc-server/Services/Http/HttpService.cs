using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using RestSharp;

namespace showdoc_server.Services.Http
{
    public class HttpService : IHttpService
    {
        private readonly IRestClient restClient;
        private readonly IRestRequest restRequest;

        public HttpService()
        {
            this.restClient = new RestClient();
            this.restRequest = new RestRequest();
        }

        public string Get(string url, Dictionary<string, string> parameters = null)
        {
            try
            {
                this.restRequest.Method = Method.GET;
                this.restClient.BaseUrl = new Uri(url);
                if (parameters != null && parameters.Count > 0)
                {
                    foreach (var parameter in parameters)
                    {
                        this.restRequest.AddQueryParameter(parameter.Key, parameter.Value);
                    }
                }
                var resp = this.restClient.Get(this.restRequest);
                if (resp.StatusCode == HttpStatusCode.OK)
                {
                    return resp.Content;
                }
                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
