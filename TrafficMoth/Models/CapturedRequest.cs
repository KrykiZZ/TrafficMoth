using System.Collections.Generic;

namespace TrafficMoth.Models
{
    public class CapturedRequest
    {
        public string Url { get; init; }
        public Dictionary<string, string> RequestHeaders { get; init; }
        public Dictionary<string, string> ResponseHeaders { get; init; }

        public string Body { get; init; }
    }
}
