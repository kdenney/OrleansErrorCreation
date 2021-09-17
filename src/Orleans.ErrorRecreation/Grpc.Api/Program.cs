using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;

namespace Grpc.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Activity.DefaultIdFormat = ActivityIdFormat.W3C;

            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHost(builder =>
                {
                    builder.UseKestrel();
                    builder.ConfigureKestrel(options =>
                        {
                            options.ListenAnyIP(7001,
                                listenOptions => listenOptions.Protocols = HttpProtocols.Http2);
                        })
                        .UseStartup<Startup>();
                })
                // .UseOrleans(builder =>
                // {
                //     builder
                //         .UseLocalhostClustering()
                //         ;
                // })
            ;
    }
}