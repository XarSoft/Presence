using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftFluent.Presence.Pushover
{
    public class PushoverResponse
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("request")]
        public string Request { get; set; }


        [JsonProperty("receipt")]
        public string Receipt { get; set; }


        [JsonProperty("errors")]
        public string[] Errors { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }
    }
}
