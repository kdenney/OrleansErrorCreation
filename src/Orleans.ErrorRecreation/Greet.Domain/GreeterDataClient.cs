using System.Threading.Tasks;
using Grains.Contracts;
using Orleans;

namespace Greet.Domain
{
    public interface IGreeterDataClient
    {
        Task<string> Greet(string name);
    }
    
    public class GreeterDataClient : IGreeterDataClient
    {
        private readonly IClusterClient _client;

        public GreeterDataClient(IGreeterOrleansFactory factory)
        {
            _client = factory.Client;
        }

        public async Task<string> Greet(string name)
        {
            var grain = _client.GetGrain<IDoGreetUserGrain>(name);
            return await grain.Greet(name);
        }
    }
}