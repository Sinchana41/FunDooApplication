using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace BessinessLogicLayer.Rabbitmq
{
    public class RabbitMqProducer
    {
        private readonly ConnectionFactory _factory;

        public RabbitMqProducer()
        {
            _factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
        }

        public void Publish<T>(string queueName, T message)
        {
            using var connection = _factory.CreateConnection(); // WORKS
            using var channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var body = Encoding.UTF8.GetBytes(
                JsonSerializer.Serialize(message)
            );

            channel.BasicPublish(
                exchange: "",
                routingKey: queueName,
                basicProperties: null,
                body: body
            );
        }
    }
}
