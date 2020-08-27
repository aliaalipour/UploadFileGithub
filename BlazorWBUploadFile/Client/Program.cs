using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BlazorWBUploadFile.Client.Interfaces;
using BlazorWBUploadFile.Client.Services;

namespace BlazorWBUploadFile.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

          
            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            string baseAddress = builder.Configuration.GetValue<string>("Url:BaseUrl");
            builder.Services.AddSingleton(new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            });


            builder.Services.AddTransient<IFileUploadService, FileUploadServices>();

            await builder.Build().RunAsync();
        }
    }
}
