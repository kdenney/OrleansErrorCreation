using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Grains.Contracts;
using Greet.Domain.Grains;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans;

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
            // var configurationBuilder = new ConfigurationBuilder()
            //     .SetBasePath(Directory.GetCurrentDirectory())
            //     .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            //
            // var configuration = configurationBuilder.Build();

            // define the cluster configuration
            var builder = new SiloHostBuilder()
                    .UseLocalhostClustering()
                    .ConfigureAppConfiguration((context, configBuilder) =>
                    {
                        // configBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);
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