using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Channels;
using SQLitePCL;

namespace DataLayer.Services
{
    /// <summary>
    /// RabbitMQ implementation of IMessagePublisherService
    /// </summary>
    public sealed class RmqPublisherService(
        IConnectionFactory factory,
        IOptions<RmqSettings> config,
        ILogger<RmqPublisherService> logger)
        : IMessagePublisherService
    {
        private readonly ILogger<RmqPublisherService> _logger = logger;

        /// <inheritdoc />
        public async Task PublishInvoiceEventAsync(string eventType, object data, CancellationToken cancellationToken = default)
        {
            await using var connection = await factory.CreateConnectionAsync(cancellationToken);
            await using var channel = await connection.CreateChannelAsync(null, cancellationToken);

            // One queue for all invoice events, if not yet declared
            await channel.QueueDeclareAsync(
                queue: "invoices.all",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null, cancellationToken: cancellationToken);

            // Bind to all invoice events, if not yet bound
            await channel.QueueBindAsync(
                queue: "invoices.all",
                exchange: "invoices.events",
                routingKey: "invoice.*", cancellationToken: cancellationToken);

            // Ensure exchange exists
            await channel.ExchangeDeclareAsync(
                exchange: config.Value.ExchangeName,
                type: "topic",
                durable: true,
                autoDelete: false,
                cancellationToken: cancellationToken);

            var routingKey = $"invoice.{eventType.ToLower()}";
            var message = JsonSerializer.Serialize(data);
            var body = Encoding.UTF8.GetBytes(message);

            await channel.BasicPublishAsync(config.Value.ExchangeName, routingKey, true, new BasicProperties
                {
                    Persistent = true,
                    ContentType = "application/json",
                    Timestamp = new AmqpTimestamp(DateTimeOffset.UtcNow.ToUnixTimeSeconds())
                },
                body, cancellationToken);
        }
    }
}