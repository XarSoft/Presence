using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftFluent.Presence.Pushover
{
    public class PushoverCancelEmergencyResponse
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("request")]
        public string Request { get; set; }
    }
}
