using System.Collections.Generic;

namespace CraftAR.SDK.Dotnet
{
    public class CatchoomSearchResponse
    {
        public IEnumerable<CatchoomResult> results { get; set; }
    }

    public class CatchoomResult
    {
        public CatchoomItem item { get; set; }
        public CatchoomImage image { get; set; }
        public int score { get; set; }
    }

    public class CatchoomImage
    {
        public string uuid { get; set; }
        public string thumb_120 { get; set; }
    }

    public class CatchoomItem
    {
        public string uuid { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string custom { get; set; }
    }
}
