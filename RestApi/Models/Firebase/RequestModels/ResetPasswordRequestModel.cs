using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestApi.Models.Firebase.RequestModels
{
    public class ResetPasswordRequestModel
    {
        [JsonPropertyName("requestType")]
        public string RequestType { get; } = "PASSWORD_RESET";

        [JsonPropertyName("email")]
        [EmailAddress(ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
    }
}
