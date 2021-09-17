using System;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;

namespace Greet.Domain
{
    public interface IGreeterOrleansFactory
    {
        IClusterClient Client { get; }
    }
    
    public class GreeterOrleansFactory : IGreeterOrleansFactory
    {
        public IClusterClient Client { get; }

        public GreeterOrleansFactory(ILogger<GreeterOrleansFactory> logger)
        {
            Client = new ClientBuilder()
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "GreeterSilo";
                })
                .Build();

            Client.Connect().GetAwaiter().GetResult();
            Console.WriteLine("Client successfully connected to silo host \n");
        }
    }
}