using Newtonsoft.Json;
using System.Collections.Generic;

namespace KoenZomers.UniFi.Api.Requests
{
    /// <summary>
    /// Enable or disable a wireless network
    /// </summary>
    public class WirelessNetworkStatus : BaseRequest
    {
        /// <summary>
        /// Is this network enabled?
        /// </summary>
        [JsonProperty(PropertyName = "enabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsEnabled { get; set; }
    }
}