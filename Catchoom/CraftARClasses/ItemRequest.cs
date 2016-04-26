using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftAR.SDK.Dotnet.CraftARClasses
{
    public class ItemRequest
    {
        public string url { get; set; }
        public string name { get; set; }
        public string collectionId { get; set; }
        public dynamic content { get; set; }
    }
}
