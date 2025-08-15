public class RmqSettings
{
    public string ConnectionString { get; init; } = string.Empty;
    public string ExchangeName { get; init; } = "invoices.events";
}