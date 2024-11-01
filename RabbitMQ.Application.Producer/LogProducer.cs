using RabbitMQ.Application.Producer.IntegrationEvents;
using RabbitMQ.Client;
using RabbitMQCommon;

namespace RabbitMQ.Application.Producer
{
    public class LogProducer : ProducerBase<LogIntegrationEvent>
    {
        public LogProducer(
            ConnectionFactory connectionFactory,
            ILogger<RabbitMqClientBase> logger,
            ILogger<ProducerBase<LogIntegrationEvent>> producerBaseLogger) :
            base(connectionFactory, logger, producerBaseLogger)
        {
        }

        protected override string ExchangeName => "CUSTOM_HOST.LoggerExchange";
        protected override string RoutingKeyName => "log.message";
        protected override string AppId => "LogProducer";
    }
}
