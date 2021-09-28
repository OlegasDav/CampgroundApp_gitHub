using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestApi.Models.Firebase.RequestModels
{
    public class SendEmailRequestModel
    {
        [JsonPropertyName("requestType")]
        public string RequestType { get; } = "VERIFY_EMAIL";

        [JsonPropertyName("idToken")]
        public string IdToken { get; set; }
    }
}
