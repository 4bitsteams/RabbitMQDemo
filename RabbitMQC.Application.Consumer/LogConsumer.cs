using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQC.Application.Consumer.Commands;
using RabbitMQCommon;

namespace RabbitMQC.Application.Consumer
{
    public class LogConsumer : ConsumerBase, IHostedService
    {
        protected override string QueueName => "CUSTOM_HOST.log.message";

        public LogConsumer(
            IMediator mediator,
            ConnectionFactory connectionFactory,
            ILogger<LogConsumer> logConsumerLogger,
            ILogger<ConsumerBase> consumerLogger,
            ILogger<RabbitMqClientBase> logger) :
            base(mediator, connectionFactory, consumerLogger, logger)
        {
            try
            {
                var consumer = new AsyncEventingBasicConsumer(Channel);
                consumer.Received += OnEventReceived<LogCommand>;
                Channel.BasicConsume(queue: QueueName, autoAck: false, consumer: consumer);
            }
            catch (Exception ex)
            {
                logConsumerLogger.LogCritical(ex, "Error while consuming message");
            }
        }

        public virtual Task StartAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        public virtual Task StopAsync(CancellationToken cancellationToken)
        {
            Dispose();
            return Task.CompletedTask;
        }
    }
}