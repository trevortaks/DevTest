using Blazored.Modal;
using Blazored.Toast;
using BlazorSpinner;
using DevTest.Client;
using DevTest.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace DevTest.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) })
                        .AddScoped<ClientService>()
                        .AddScoped<ContactService>()
                        .AddScoped<RequestClient>()
                        .AddScoped<SpinnerService>()
                        .AddBlazoredModal()
                        .AddBlazoredToast();

            await builder.Build().RunAsync();
        }
    }
}