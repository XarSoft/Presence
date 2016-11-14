using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftFluent.Presence.Pushover
{
    public class PushoverCallbackRequest
    {
        [JsonProperty("receipt")]
        public string Receipt { get; set; }

        [JsonProperty("acknowledged")]
        public int Acknowledged { get; set; }

        [JsonProperty("acknowledged_at")]
        public int AcknowledgedAt { get; set; }

        [JsonProperty("acknowledged_by")]
        public string AcknowledgedBy { get; set; }

        [JsonProperty("acknowledged_by_device")]
        public string AcknowledgedByDevice { get; set; }
    }
}
