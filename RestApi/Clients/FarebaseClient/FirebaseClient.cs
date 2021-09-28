using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using RestApi.Models.Firebase.RequestModels;
using RestApi.Models.Firebase.ResponseModels;
using RestApi.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace RestApi.Clients.FarebaseClient
{
    public class FirebaseClient : IFirebaseClient
    {
        private readonly HttpClient _httpClient;
        private readonly ApiKeySettings _apiKeySettings;

        public FirebaseClient(HttpClient httpClient, IOptions<ApiKeySettings> apiKeySettings)
        {
            _httpClient = httpClient;
            _apiKeySettings = apiKeySettings.Value;
        }
        public async Task<SignUpResponseModel> SignUpAsync(SignUpRequestModel user)
        {
            var url = $"{_apiKeySettings.BaseAddress}:signUp?key={_apiKeySettings.ApiKey}";

            var response = await _httpClient.PostAsJsonAsync(url, user);

            if (!response.IsSuccessStatusCode)
            {
                var newError= await response.Content.ReadFromJsonAsync<ErrorResponseModel>();
                throw new BadHttpRequestException($"{newError.Error.Message}", newError.Error.Code);
            }

            return await response.Content.ReadFromJsonAsync<SignUpResponseModel>();
        }

        public async Task<SignInResponseModel> SignInAsync(SignInRequestModel user)
        {
            var url = $"{_apiKeySettings.BaseAddress}:signInWithPassword?key={_apiKeySettings.ApiKey}";

            var response = await _httpClient.PostAsJsonAsync(url, user);

            if (!response.IsSuccessStatusCode)
            {
                var newError = await response.Content.ReadFromJsonAsync<ErrorResponseModel>();
                throw new BadHttpRequestException($"{newError.Error.Message}", newError.Error.Code);
            }

            return await response.Content.ReadFromJsonAsync<SignInResponseModel>();
        }

        public async Task<SendEmailResponseModel> SendEmailAsync(SendEmailRequestModel user)
        {
            var url = $"{_apiKeySettings.BaseAddress}:sendOobCode?key={_apiKeySettings.ApiKey}";

            var response = await _httpClient.PostAsJsonAsync(url, user);

            if (!response.IsSuccessStatusCode)
            {
                var newError = await response.Content.ReadFromJsonAsync<ErrorResponseModel>();
                throw new BadHttpRequestException($"{newError.Error.Message}", newError.Error.Code);
            }

            return await response.Content.ReadFromJsonAsync<SendEmailResponseModel>();
        }

        public async Task<ResetPasswordResponseModel> ResetPasswordAsync(ResetPasswordRequestModel user)
        {
            var url = $"{_apiKeySettings.BaseAddress}:sendOobCode?key={_apiKeySettings.ApiKey}";

            var response = await _httpClient.PostAsJsonAsync(url, user);

            if (!response.IsSuccessStatusCode)
            {
                var newError = await response.Content.ReadFromJsonAsync<ErrorResponseModel>();
                throw new BadHttpRequestException($"{newError.Error.Message}", newError.Error.Code);
            }

            return await response.Content.ReadFromJsonAsync<ResetPasswordResponseModel>();
        }

        public async Task<ChangeEmailResponseModel> ChangeEmailAsync(ChangeEmailRequestModel user)
        {
            var url = $"{_apiKeySettings.BaseAddress}:update?key={_apiKeySettings.ApiKey}";

            var response = await _httpClient.PostAsJsonAsync(url, user);

            if (!response.IsSuccessStatusCode)
            {
                var newError = await response.Content.ReadFromJsonAsync<ErrorResponseModel>();
                throw new BadHttpRequestException($"{newError.Error.Message}", newError.Error.Code);
            }

            return await response.Content.ReadFromJsonAsync<ChangeEmailResponseModel>();
        }

        public async Task<ChangePasswordResponseModel> ChangePasswordAsync(ChangePasswordRequestModel user)
        {
            var url = $"{_apiKeySettings.BaseAddress}:update?key={_apiKeySettings.ApiKey}";

            var response = await _httpClient.PostAsJsonAsync(url, user);

            if (!response.IsSuccessStatusCode)
            {
                var newError = await response.Content.ReadFromJsonAsync<ErrorResponseModel>();
                throw new BadHttpRequestException($"{newError.Error.Message}", newError.Error.Code);
            }

            return await response.Content.ReadFromJsonAsync<ChangePasswordResponseModel>();
        }
    }
}
