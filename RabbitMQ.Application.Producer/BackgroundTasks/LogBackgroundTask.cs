﻿using RabbitMQ.Application.Producer.IntegrationEvents;
using RabbitMQCommon;

namespace RabbitMQ.Application.Producer.BackgroundTasks
{
    public class LogBackgroundTask : BackgroundService
    {
        private readonly IRabbitMqProducer<LogIntegrationEvent> _producer;

        public LogBackgroundTask(IRabbitMqProducer<LogIntegrationEvent> producer) => _producer = producer;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var @event = new LogIntegrationEvent
                {
                    Id = Guid.NewGuid(),
                    Message = $"Hello! Message generated at {DateTime.Now.ToString("O")}"
                };

                _producer.Publish(@event);
                await Task.Delay(20000, stoppingToken);
            }

            await Task.CompletedTask;
        }
    }
}
