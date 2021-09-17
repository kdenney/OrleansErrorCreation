using System.Threading.Tasks;
using Grains.Contracts;
using Orleans;

namespace Greet.Domain.Grains
{
    public class DoGreetUserGrain : Grain, IDoGreetUserGrain
    {
        // here we would use a repository to get from a database or something

        public Task<string> Greet(string name)
        {
            return Task.FromResult($"Hello {name}!!");
        }
    }
}