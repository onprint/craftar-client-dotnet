using System;
using System.IO;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using NUnit.Framework;

namespace CraftAR.SDK.Dotnet.IntegrationTests
{
    [TestFixture()]
    public class CraftARClientTests
    {
        private CraftARConfiguration Configuration
        {
            get
            {
                return new CraftARConfiguration()
                {
                    APIKey = "YOUR API KEY",
                    DefaultCollectionId = "YOUR DEFAULT COLLECTION ID",
                    HostModify = @"https://my.craftar.net/api/v0",
                    HostSearch = @"https://search.craftar.net/v1"
                };
            }
        }

        [Test()]
        public void CreateItemTest()
        {
            var client = new CraftARClient(Configuration);
            string result = client.PostItem("Item Test").Content.ReadAsStringAsync().Result;
            string itemId = Convert.ToString(((dynamic)JsonConvert.DeserializeObject(result)).uuid);

            Assert.IsTrue(!String.IsNullOrWhiteSpace(itemId));

            client.DeleteItem(itemId);
        }

        [Test()]
        public void DeleteItemTest()
        {
            var client = new CraftARClient(Configuration);
            string result = client.PostItem("Item Test").Content.ReadAsStringAsync().Result;
            string itemId = Convert.ToString(((dynamic)JsonConvert.DeserializeObject(result)).uuid);

            if (!String.IsNullOrWhiteSpace(itemId))
            {
                HttpResponseMessage response = client.DeleteItem(itemId);

                Assert.IsTrue(response.StatusCode == HttpStatusCode.NoContent);
            }
        }

        [Test()]
        public void CreateImageTest()
        {
            var client = new CraftARClient(Configuration);
            string imagePath = @"..\Debug\cloud.jpeg";
            byte[] imageContent = File.ReadAllBytes(imagePath);
            string result = client.PostItem("Image Test").Content.ReadAsStringAsync().Result;
            string itemId = Convert.ToString(((dynamic)JsonConvert.DeserializeObject(result)).uuid);

            HttpResponseMessage response = client.PostImage(imageContent, itemId);

            Assert.IsTrue(response.StatusCode == HttpStatusCode.Created);

            client.DeleteItem(itemId);
        }

        [Test()]
        public void DeleteImageTest()
        {
            var client = new CraftARClient(Configuration);
            string imagePath = @"..\Debug\water.png";
            byte[] imageContent = File.ReadAllBytes(imagePath);
            string itemResult = client.PostItem("Image Test").Content.ReadAsStringAsync().Result;
            string itemId = Convert.ToString(((dynamic)JsonConvert.DeserializeObject(itemResult)).uuid);
            string imageResult = client.PostImage(imageContent, itemId).Content.ReadAsStringAsync().Result;
            string imageId = Convert.ToString(((dynamic)JsonConvert.DeserializeObject(imageResult)).uuid);

            HttpResponseMessage response = client.DeleteImage(imageId);

            Assert.IsTrue(response.StatusCode == HttpStatusCode.NoContent);

            client.DeleteItem(itemId);
        }
    }
}
