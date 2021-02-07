using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Raat.Api.Contracts;

namespace Raat.Api.Services
{
    public static class ServiceRegistry
    {
        public static void RegisterService(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            serviceCollection.AddScoped<IRequestContext, RequestContext>();
        }
    }
}
