using Microsoft.Extensions.DependencyInjection;
using RestApi.Clients.FarebaseClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddClients(this IServiceCollection services)
        {
            services.AddHttpClient<IFirebaseClient, FirebaseClient>();

            return services;
        }
    }
}
