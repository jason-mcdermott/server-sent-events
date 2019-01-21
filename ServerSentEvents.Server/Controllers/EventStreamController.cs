using System.Collections.Concurrent;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using ServerSentEvents.Server.Services;

namespace ServerSentEvents.Server.Controllers
{
    [Route("api/stream")]
    public class EventStreamController : ApiController
    {
        EventStreamService _eventStreamService;
        
        public EventStreamController()
        {
            _eventStreamService = new EventStreamService();
        }

        
        // This is based on code found here: https://stackoverflow.com/a/44855299
        public HttpResponseMessage GetEvents(CancellationToken clientDisconnectToken)
        {
            var response = Request.CreateResponse();
            response.Content = 
                new PushStreamContent(async (stream, httpContent, transportContext) =>
                {
                    using (var writer = new StreamWriter(stream))
                    {
                        using (var consumer = new BlockingCollection<string>())
                        {
                            var eventGeneratorTask = _eventStreamService.OpenStreamAsync(consumer, clientDisconnectToken);

                            foreach (var @event in consumer.GetConsumingEnumerable(clientDisconnectToken))
                            {
                                //await writer.WriteLineAsync("data: " + @event);
                                //await writer.WriteLineAsync();
                                await writer.WriteLineAsync(@event);
                                await writer.FlushAsync();
                            }

                            await eventGeneratorTask;
                        }
                    }
                }, "text/event-stream");

            return response;
        }
    }
}
