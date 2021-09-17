
using System;
using Grpc.Api;
using Grpc.Net.Client;

namespace Grpc.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("http://localhost:7001", new GrpcChannelOptions
            {
            });
            
            var client = new Greeter.GreeterClient(channel);

            var reply = client.SayHello(new HelloRequest
            {
                Name = "Kyle Deezy"
            });
            
            Console.WriteLine($"Service replied with: {reply.Message}");


        }
    }
}