using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Blazored.LocalStorage;

namespace KanaTester
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddBlazoredLocalStorage();

            AddServices(builder.Services);

            var host = builder.Build();

            await host.Services.GetRequiredService<ISymbolRepository>().Init();
            
            await host.RunAsync();
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<ISymbolRepository, SymbolRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
        }
    }
}
