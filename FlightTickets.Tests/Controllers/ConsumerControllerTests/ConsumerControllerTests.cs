using FlightTickets.ConsumerAPI.Controllers;
using FlightTickets.ConsumerAPI.Data;
using FlightTickets.ConsumerAPI.Repository;
using FlightTickets.ConsumerAPI.Repository.Interfaces;
using FlightTickets.ConsumerAPI.Services;
using FlightTickets.ConsumerAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FlightTickets.Tests.Controllers.ConsumerControllerTests
{
    public class ConsumerControllerTests
    {
        private readonly LoggerFactory _loggerFactory = new LoggerFactory();
        private readonly IConsumerRepository _consumerRepository;
        private readonly IConsumerService _consumerService;
        private readonly ConsumerController _consumerController;

        public ConsumerControllerTests()
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
            _consumerRepository = new ConsumerRepository(_loggerFactory.CreateLogger<ConsumerRepository>(), connectionDb);
            _consumerService = new ConsumerService(_loggerFactory.CreateLogger<ConsumerService>(), _consumerRepository);
            _consumerController = new ConsumerController(_loggerFactory.CreateLogger<ConsumerController>(), _consumerService);
        }

        [Fact]
        public void ConsumerTicketSaveInDatabase()
        {
            // Act
            var result = _consumerController.TicketSaveDatabase();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result.Result);
        }
    }
}
