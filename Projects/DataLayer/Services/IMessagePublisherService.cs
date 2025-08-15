using System.Threading;

namespace DataLayer.Services
{
    /// <summary>
    /// Service for publishing messages to message broker
    /// </summary>
    public interface IMessagePublisherService
    {
        /// <summary>
        /// Publishes an invoice event message
        /// </summary>
        /// <param name="eventType">Type of event (added, updated, deleted)</param>
        /// <param name="data">Data to publish</param>
        /// <param name="cancellationToken">Cancellation token</param>
        Task PublishInvoiceEventAsync(string eventType, object data, CancellationToken cancellationToken = default);
    }
}