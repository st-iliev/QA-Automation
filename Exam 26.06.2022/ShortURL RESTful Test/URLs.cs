using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShortURL_RESTful_Test
{
    public class URLs
    {
        [JsonPropertyName("url")]
        public string url { get; set; }

        [JsonPropertyName("shortCode")]
        public string shortCode { get; set; }
        [JsonPropertyName("shortUrl")]
        public string shortUrl { get; set; }
    }
}
