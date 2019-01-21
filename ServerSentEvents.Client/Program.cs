using System.Threading.Tasks;

namespace ServerSentEvents.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var listener = new EventListener();
            await listener.ProcessAsync();
        }
    }
}
