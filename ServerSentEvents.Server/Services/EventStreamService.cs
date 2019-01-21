using System;
using System.Collections.Concurrent;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerSentEvents.Server.Services
{
    public class EventStreamService
    {
        private readonly string[] _data = new string[]
        {
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
            "Nunc pulvinar sapien et ligula ullamcorper malesuada proin.",
            "Habitasse platea dictumst vestibulum rhoncus est pellentesque elit ullamcorper dignissim.",
            "Fringilla phasellus faucibus scelerisque eleifend.",
            "Suspendisse in est ante in nibh mauris cursus mattis molestie.",
            "Ultrices vitae auctor eu augue ut."
        };
        
        public async Task OpenStreamAsync(BlockingCollection<string> producer, CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var stringBuilder = new StringBuilder();
                    
                    while (true)
                    {
                        foreach (var line in _data)
                        {
                            //UTF8Encoding encoding = new UTF8Encoding();
                            //byte[] buf = encoding.GetBytes(line);

                            //foreach (byte b in buf)
                            //{
                            //    stringBuilder.Append(Convert.ToString(b, 2));
                            //}

                            //producer.Add(stringBuilder.ToString());
                            producer.Add(line);
                            await Task.Delay(1000, cancellationToken).ConfigureAwait(false);
                        }
                    }
                }
            }
            finally
            {
                producer.CompleteAdding();
            }
        }
    }
}