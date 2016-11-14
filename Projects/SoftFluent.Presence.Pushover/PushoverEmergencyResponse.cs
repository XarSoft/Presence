using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftFluent.Presence.Pushover
{
    public class PushoverEmergencyResponse
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("acknowledged")]
        public bool Acknowledged { get; set; }

        [JsonProperty("acknowledged_at")]
        public int AcknowledgedAtUtc { get; set; }

        [JsonProperty("acknowledged_by")]
        public string AcknowledgedBy { get; set; }

        [JsonProperty("acknowledged_by_device")]
        public string AcknowledgedByDevice { get; set; }

        [JsonProperty("last_delivered_at")]
        public int LastDeliveredAt { get; set; }

        [JsonProperty("expired")]
        public bool Expired { get; set; }

        [JsonProperty("expires_at")]
        public int ExpiresAtUtc { get; set; }

        [JsonProperty("called_back")]
        public int CalledBack { get; set; }

        [JsonProperty("called_back_at")]
        public int CalledBackAt { get; set; }

        [JsonProperty("request")]
        public string Request { get; set; }
    }
}
