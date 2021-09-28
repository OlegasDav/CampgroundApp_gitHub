using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence.Models.WriteModels;
using Persistence.Repositories;
using RestApi.Clients.FarebaseClient;
using RestApi.Models.Firebase.RequestModels;
using RestApi.Models.Firebase.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IFirebaseClient _firebaseClient;
        private readonly IUserRepository _userRepository;

        public AuthController(IFirebaseClient firebaseClient, IUserRepository userRepository)
        {
            _firebaseClient = firebaseClient;
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("signUp")]
        public async Task<ActionResult<SignUpResponseModel>> SignUp([FromBody] SignUpRequestModel request)
        {
            try
            {
                var userInfo = await _firebaseClient.SignUpAsync(request);

                var verificationEmail = new SendEmailRequestModel
                {
                    IdToken = userInfo.IdToken
                };

                await _firebaseClient.SendEmailAsync(verificationEmail);

                var userNew = new UserWriteModel
                {
                    Id = Guid.NewGuid(),
                    Email = userInfo.Email,
                    LocalId = userInfo.LocalId
                };

                await _userRepository.SaveUserAsync(userNew);

                return userInfo;
            }
            catch(BadHttpRequestException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        [Route("signIn")]
        public async Task<ActionResult<SignInResponseModel>> SignIn([FromBody] SignInRequestModel request)
        {
            try
            {
                return await _firebaseClient.SignInAsync(request);
            }
            catch (BadHttpRequestException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        //[HttpPost]
        //[Route("sendEmail")]
        //public async Task<ActionResult<SendEmailResponseModel>> SendEmail([FromBody] SendEmailRequestModel request)
        //{
        //    try
        //    {
        //        return await _firebaseClient.SendEmailAsync(request);
        //    }
        //    catch (BadHttpRequestException exception)
        //    {
        //        return BadRequest(exception.Message);
        //    }
        //}

        [HttpPost]
        [Route("resetPassword")]
        public async Task<ActionResult<ResetPasswordResponseModel>> ResetPassword([FromBody] ResetPasswordRequestModel request)
        {
            try
            {
                return await _firebaseClient.ResetPasswordAsync(request);
            }
            catch (BadHttpRequestException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        [Route("changeEmail")]
        public async Task<ActionResult<ChangeEmailResponseModel>> ChangeEmail([FromBody] ChangeEmailRequestModel request)
        {
            try
            {
                var userInfo = await _firebaseClient.ChangeEmailAsync(request);

                var verificationEmail = new SendEmailRequestModel
                {
                    IdToken = userInfo.IdToken
                };

                await _firebaseClient.SendEmailAsync(verificationEmail);

                return userInfo;
            }
            catch (BadHttpRequestException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        [Route("changePassword")]
        public async Task<ActionResult<ChangePasswordResponseModel>> ChangePassword([FromBody] ChangePasswordRequestModel request)
        {
            try
            {
                return await _firebaseClient.ChangePasswordAsync(request);
            }
            catch (BadHttpRequestException exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
