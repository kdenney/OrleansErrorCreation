using System.Threading.Tasks;
using Orleans;

namespace Grains.Contracts
{
    public interface IDoGreetUserGrain : IGrainWithStringKey
    {
        Task<string> Greet(string name);
    }
}