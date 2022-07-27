using RabbitMQ.Client;
using SwiftUserManagement.API.Entities;
using System.Text;
using System.Text.Json;

namespace SwiftUserManagement.API.Repositories
{
    // Concrete class for emitting tasks out to the rabbitMQ queue
    public class RabbitMQRepository : IRabbitMQRepository
    {
        private readonly ILogger<RabbitMQRepository> _Logger;

        public RabbitMQRepository(ILogger<RabbitMQRepository> ILogger)
        {
            _Logger = ILogger ?? throw new ArgumentNullException(nameof(ILogger));
        }

        // Sending out the game score analysis task to the queue
        public bool EmitGameAnalysis(GameResults gameResults)
        {
            if (gameResults.result1 == null)
            {
                return false;
            }

            // Connecting to the RabbitMQ queue
            var factory = new ConnectionFactory() { HostName = "rabbitmq" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                _Logger.LogInformation("Sending game results for analysis");
                // Setting up and sending the message
                channel.ExchangeDeclare(exchange: "swift_rehab_app",
                                        type: "topic");

                var routingKey = "game.score.fromApp";
                var message = JsonSerializer.Serialize(gameResults);
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "swift_rehab_app",
                                     routingKey: routingKey,
                                     basicProperties: null,
                                     body: body);
                _Logger.LogInformation("Sent game results for analysis");

                return true;
            }
        }
    }
}
