﻿using RestApi.Models.Firebase.RequestModels;
using RestApi.Models.Firebase.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Clients.FarebaseClient
{
    public interface IFirebaseClient
    {
        Task<SignUpResponseModel> SignUpAsync(SignUpRequestModel user);

        Task<SignInResponseModel> SignInAsync(SignInRequestModel user);
    }
}
