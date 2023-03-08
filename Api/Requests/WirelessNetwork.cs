using Newtonsoft.Json;
using System.Collections.Generic;

namespace KoenZomers.UniFi.Api.Requests
{
    /// <summary>
    /// Set properties on the wireless network
    /// </summary>
    public class WirelessNetwork : BaseRequest
    {
        /// <summary>
        /// Is this network enabled?
        /// </summary>
        [JsonProperty(PropertyName = "enabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsEnabled { get; set; } = null;

        /// <summary>
        /// Is a MAC-Filter enabled?
        /// </summary>
        [JsonProperty(PropertyName = "mac_filter_enabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsMACFilterEnabled { get; set; } = null;

        /// <summary>
        /// Default MAC-Filter policy
        /// </summary>
        [JsonProperty(PropertyName = "mac_filter_policy", NullValueHandling = NullValueHandling.Ignore)]
        public string MACFilterPolicy { get; set; }

        /// <summary>
        /// List of MAC addresses
        /// </summary>
        [JsonProperty(PropertyName = "mac_filter_list", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> MACFilterList { get; set; }
    }
}