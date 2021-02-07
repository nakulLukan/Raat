using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Raat.Web.Contracts;
using Raat.Web.JSServices;
using Raat.Web.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Raat.Web
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.ServiceRegistry();
            await builder.Build().RunAsync();
        }

        private static void ServiceRegistry(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddScoped<ClipboardService>();
            builder.Services.AddScoped<IHttpContext, HttpContext>();
        }
    }
}
