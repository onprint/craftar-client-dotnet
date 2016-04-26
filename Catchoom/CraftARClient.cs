using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Remoting;
using System.Text;
using CraftAR.SDK.Dotnet.CraftARClasses;
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

        #region Image
        public Image CreateImage(ImageRequest imageRequest)
        {
            if (imageRequest == null)
            {
                throw new ArgumentNullException("imageRequest");
            }

            using (var client = new HttpClient())
            {
                var content = new MultipartFormDataContent();

                var fileContent = new ByteArrayContent(imageRequest.content);

                fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    Name = "file",
                    FileName = imageRequest.filename
                };
                content.Add(fileContent, "file");

                string itemContent = String.Format("/api/v0/item/{0}/", imageRequest.itemId);
                string uri = String.Format("{0}/image/?api_key={1}", Configuration.HostModify, Configuration.APIKey);

                content.Add(new StringContent(itemContent), "item");

                var result = client.PostAsync(uri, content).Result;
                if (result.StatusCode == HttpStatusCode.Created)
                {
                    return JsonConvert.DeserializeObject<Image>(result.Content.ReadAsStringAsync().Result);
                }
                return null;
            }
        }

        public bool DeleteImage(string imageId)
        {
            if (imageId == null)
            {
                throw new ArgumentNullException("imageId");
            }

            using (var client = new HttpClient())
            {
                var uri = String.Format("{0}/image/{1}/?api_key={2}", Configuration.HostModify, imageId, Configuration.APIKey);

                return client.DeleteAsync(uri).Result.StatusCode == HttpStatusCode.NoContent;
            }
        }

        #endregion

        #region Item
        public Item CreateItem(ItemRequest itemRequest)
        {
            if (itemRequest == null)
            {
                throw new ArgumentNullException("itemRequest");
            }

            using (var client = new HttpClient())
            {
                var item = new
                {
                    name = itemRequest.name,
                    collection = String.Format("/api/v0/collection/{0}/", itemRequest.collectionId ?? Configuration.DefaultCollectionId),
                    url= itemRequest.url,
                    content = itemRequest.content
                };
                string uri = String.Format("{0}/item/?api_key={1}", Configuration.HostModify, Configuration.APIKey);

                var content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");

                var result = client.PostAsync(uri, content).Result;
                if (result.StatusCode == HttpStatusCode.Created)
                {
                    return JsonConvert.DeserializeObject<Item>(result.Content.ReadAsStringAsync().Result);
                }
                return null;
            }
        }

        public bool DeleteItem(string itemId)
        {
            if (itemId == null)
            {
                throw new ArgumentNullException("itemId");
            }

            using (var client = new HttpClient())
            {
                string uri = String.Format("{0}/item/{1}/?api_key={2}", Configuration.HostModify, itemId, Configuration.APIKey);
                return client.DeleteAsync(uri).Result.StatusCode == HttpStatusCode.NoContent;
            }
        }
        #endregion

        public HttpResponseMessage SearchImage(HttpContent content)
        {
            using (var client = new HttpClient())
            {
                return client.PostAsync(String.Format("{0}/search", Configuration.HostSearch), content).Result;
            }
        }
    }
}
