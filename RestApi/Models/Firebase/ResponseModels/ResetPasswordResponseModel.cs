using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestApi.Models.Firebase.ResponseModels
{
    public class ResetPasswordResponseModel
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}
