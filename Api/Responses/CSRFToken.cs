using Newtonsoft.Json;
using System.Collections.Generic;

namespace KoenZomers.UniFi.Api.Responses
{
    /// <summary>
    /// JWT CSRF token data returned in TOKEN header
    /// </summary>
    public class CSRFToken : BaseResponse
    {
        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "csrfToken")]
        public string Token { get; set; }

        [JsonProperty(PropertyName = "jti")]
        public string Jti { get; set; }

        [JsonProperty(PropertyName = "passwordRevision")]
        public long PasswordRevision { get; set; }

        [JsonProperty(PropertyName = "iat")]
        public long IssuedAt { get; set; }

        [JsonProperty(PropertyName = "exp")]
        public long Expiration { get; set; }
    }
}