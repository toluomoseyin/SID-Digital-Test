namespace AccountMgt.Infrastructure.Options
{
    public class RabbitMqConfig
    {
        public required string Host { get; set; }

        public int Port { get; set; }

        public required string VirtualHost { get; set; }

        public required string Username { get; set; }

        public required string Password { get; set; }
    }
}
