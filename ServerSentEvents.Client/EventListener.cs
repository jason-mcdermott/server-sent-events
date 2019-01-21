using System;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ServerSentEvents.Client
{
    public class EventListener
    {
        public async Task ProcessAsync()
        {
            using (var client = new HttpClient())
            {
                using (var stream = await client.GetStreamAsync(ConfigurationManager.AppSettings["stream-endpoint"]))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        while (true)
                        {
                            Console.WriteLine(reader.ReadLine());
                        }
                    }
                }
            }
        }
    }
}
