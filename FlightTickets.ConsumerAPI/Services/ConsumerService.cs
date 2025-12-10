using FlightTickets.ConsumerAPI.Repository.Interfaces;
using FlightTickets.ConsumerAPI.Services.Interfaces;
using FlightTickets.Models.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace FlightTickets.ConsumerAPI.Services
{
    public class ConsumerService : IConsumerService
    {
        private readonly ILogger<ConsumerService> _logger;
        private readonly IConsumerRepository _consumerRepository;

        public ConsumerService
        (
            ILogger<ConsumerService> logger,
            IConsumerRepository consumerRepository
        )
        {
            _logger = logger;
            _consumerRepository = consumerRepository;
        }

        public async Task GetTicketsFromQueuesAsync()
        {
            try
            {
                var factory = new ConnectionFactory { HostName = "localhost" };
                using var connection = await factory.CreateConnectionAsync();
                using var channel = await connection.CreateChannelAsync();

                await channel.QueueDeclareAsync
                (
                    queue: "ticketsApproved",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                var consumer = new AsyncEventingBasicConsumer(channel);
                consumer.ReceivedAsync += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    var ticket = JsonSerializer.Deserialize<Ticket>(message);
                    await SaveApprovedTicketsToCollectionAsync(ticket);
                };

                await channel.BasicConsumeAsync
                (
                    queue: "ticketsApproved",
                    autoAck: true,
                    consumer: consumer
                );



                await channel.QueueDeclareAsync
                (
                    queue: "ticketsDenied",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                consumer = new AsyncEventingBasicConsumer(channel);
                consumer.ReceivedAsync += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    var ticket = JsonSerializer.Deserialize<Ticket>(message);
                    await SaveDeniedTicketsToCollectionAsync(ticket);
                };

                await channel.BasicConsumeAsync
                (
                    queue: "ticketsDenied",
                    autoAck: true,
                    consumer: consumer
                );
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurring while processing the queues");
                throw;
            }
        }

        public async Task SaveApprovedTicketsToCollectionAsync(Ticket ticket)
        {
            try
            {
                await _consumerRepository.SaveApprovedTicketsAsync(ticket);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurring while save approved tickets in Data " + ex.Message);
            }
        }

        public async Task SaveDeniedTicketsToCollectionAsync(Ticket ticket)
        {
            try
            {
                await _consumerRepository.SaveDeniedTicketsAsync(ticket);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurring while save denied tickets in Data " + ex.Message);
            }
        }
    }
}
