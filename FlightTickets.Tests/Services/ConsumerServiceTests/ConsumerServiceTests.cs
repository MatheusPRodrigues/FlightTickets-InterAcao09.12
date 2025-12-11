using Bogus;
using FlightTickets.ConsumerAPI.Data;
using FlightTickets.ConsumerAPI.Repository;
using FlightTickets.ConsumerAPI.Repository.Interfaces;
using FlightTickets.ConsumerAPI.Services;
using FlightTickets.Models.Models;
using FlightTickets.Models.Models.DTOs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FlightTickets.Tests.Services.ConsumerServiceTests
{
    public class ConsumerServiceTests
    {
        private readonly Faker _faker = new Faker("pt_BR");
        private readonly LoggerFactory _loggerFactory = new LoggerFactory();
        private readonly IConsumerRepository _consumerRepository;
        private readonly ConsumerService _consumerService;

        public ConsumerServiceTests()
        {
            var settings = Options.Create(
                new MongoDBSettings
                {
                    ConnectionURI = "mongodb+srv://matherodrigues17_db_user:23McUhyzN6C1IE2x@cluster0.3ulpzke.mongodb.net/",
                    DatabaseName = "FlightTickets",
                    TicketApprovedCollection = "TicketsApproved",
                    TicketDeniedCollection = "TicketsDenied"
                }
            );
            var connectionDb = new ConnectionDB(settings);

            _consumerRepository = new ConsumerRepository(
                _loggerFactory.CreateLogger<ConsumerRepository>(),
                connectionDb
            );
            _consumerService = new ConsumerService(
                _loggerFactory.CreateLogger<ConsumerService>(),
                _consumerRepository
            );
        }

        [Fact]
        public async Task ConsumeTicketsFromQueue()
        {
            //Act
            var result = _consumerService.GetTicketsFromQueuesAsync();
            var exception = Record.ExceptionAsync(() => result).Result;

            // Assert
            Assert.NotNull(result);
            Assert.Null(exception);
        }

        [Fact]
        public async Task teste1()
        {
            // Arrange
            var ticket = new Ticket
            (
                _faker.Name.FullName(),
                _faker.Random.AlphaNumeric(6).ToUpper(),
                $"{_faker.Random.Int(1, 30)}{_faker.Random.Char('A', 'F')}",
                _faker.Finance.Amount(600, 6000, 2)
            );

            // Act
            var result = _consumerService.SaveApprovedTicketsToCollectionAsync(ticket);
            
            // Assert
            Assert.NotNull(result);
            
            //Assert.Equal(Task.CompletedTask.IsCompleted, result.AsyncState);
        }
    }
}
