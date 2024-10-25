using MassTransit;
using MassTransitWebApi.Models;

namespace MassTransitWebApi.Consumers
{
    public class SampleMessageConsumer : IConsumer<SampleMessage>
    {
        private readonly ILogger<SampleMessageConsumer> _logger;

        public SampleMessageConsumer(ILogger<SampleMessageConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<SampleMessage> context)
        {
            _logger.LogInformation("Received message: {Text}", context.Message.Text);
            // Handle the message...
        }
    }
}
