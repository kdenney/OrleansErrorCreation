using System;
using System.Net;
using System.Threading.Tasks;
using Grains.Contracts;
using Greet.Domain.Grains;
using Microsoft.Extensions.DependencyInjection;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;

namespace Greet.Silo
{
    class Program
    {
        static int Main(string[] args)
        {
            return MainAsync().GetAwaiter().GetResult();
        }
        
        private static async Task<int> MainAsync()
        {
            try
            {
                var host = await StartSilo();
                Console.WriteLine("\n\n Press Enter to terminate...\n\n");
                Console.ReadLine();

                await host.StopAsync();

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 1;
            }
        }

        private static async Task<ISiloHost> StartSilo()
        {
            // define the cluster configuration
            var builder = new SiloHostBuilder()
                    .UseLocalhostClustering()
                    .ConfigureAppConfiguration((context, configBuilder) =>
                    {
                    })
                    .ConfigureServices((context, services) =>
                    {
                        services.AddTransient<IDoGreetUserGrain, DoGreetUserGrain>();
                    })
                    .AddSimpleMessageStreamProvider("SMSProvider")
                    .AddMemoryGrainStorage("PubSubStore")
                    .Configure<ClusterOptions>(options =>
                    {
                        options.ClusterId = "dev";
                        options.ServiceId = "GreeterSilo";
                    })
                    .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(DoGreetUserGrain).Assembly).WithReferences())
                    .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
                ;

            var host = builder.Build();

            await host.StartAsync();
            return host;
        }

    }
}