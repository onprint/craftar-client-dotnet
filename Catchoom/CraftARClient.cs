using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace CraftAR.SDK.Dotnet
{
    public class CraftARClient
    {
        public CraftARConfiguration Configuration { get; set; }

        public CraftARClient()
        { }

        public CraftARClient(CraftARConfiguration configuration)
        {
            Configuration = configuration;
        }

        public HttpResponseMessage PostImage(byte[] image, string itemId)
        {
            using (var client = new HttpClient())
            {
                var content = new MultipartFormDataContent();
                var fileContent = new ByteArrayContent(image);

                fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    Name = "file",
                    FileName = "\"Catchoom\""
                };
                content.Add(fileContent, "file");

                string itemContent = String.Format("/api/v0/item/{0}/", itemId);
                string uri = String.Format("{0}/image/?api_key={1}", Configuration.HostModify, Configuration.APIKey);

                content.Add(new StringContent(itemContent), "item");

                return client.PostAsync(uri, content).Result;
            }
        }

        public HttpResponseMessage DeleteImage(string id)
        {
            using (var client = new HttpClient())
            {
                var uri = String.Format("{0}/image/{1}/?api_key={2}", Configuration.HostModify, id, Configuration.APIKey);

                return client.DeleteAsync(uri).Result;
            }
        }

        public HttpResponseMessage SearchImage(HttpContent content)
        {
            using (var client = new HttpClient())
            {
                return client.PostAsync(String.Format("{0}/search", Configuration.HostSearch), content).Result;
            }
        }

        public HttpResponseMessage PostItem(string name)
        {
            using (var client = new HttpClient())
            {
                var item = new
                {
                    name = name,
                    collection = String.Format("/api/v0/collection/{0}/", Configuration.DefaultCollectionId)
                };
                string uri = String.Format("{0}/item/?api_key={1}", Configuration.HostModify, Configuration.APIKey);
                var content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");

                return client.PostAsync(uri, content).Result;
            }
        }

        public HttpResponseMessage DeleteItem(string id)
        {
            using (var client = new HttpClient())
            {
                string uri = String.Format("{0}/item/{1}/?api_key={2}", Configuration.HostModify, id, Configuration.APIKey);

                return client.DeleteAsync(uri).Result;
            }
        }
    }
}
