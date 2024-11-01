#nullable disable
namespace RabbitMQ.Application.Producer.IntegrationEvents
{
    public class LogIntegrationEvent
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
    }
}
