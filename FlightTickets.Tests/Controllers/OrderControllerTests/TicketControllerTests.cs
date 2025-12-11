using Bogus;
using FlightTickets.Models.Models.DTOs;
using FlightTickets.OrderAPI.Controllers;
using FlightTickets.OrderAPI.Services;
using FlightTickets.OrderAPI.Services.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FlightTickets.Tests.Controllers.OrderControllerTests
{
    public class TicketControllerTests
    {
        private readonly Faker _faker = new Faker("pt_BR");
        private readonly ILogger<TicketController> _logger;
        private readonly ITicketService _ticketService;
        private readonly TicketController _ticketController;

        public TicketControllerTests()
        {
            _logger = new LoggerFactory().CreateLogger<TicketController>();
            _ticketService = new TicketService();
            _ticketController = new TicketController(_logger, _ticketService);
        }

        [Fact]
        public void TicketControllerCreateTicketReturn()
        {
            // Arrange
            var ticketRequest = new TicketRequestDTO
            {
                PassengerName = _faker.Name.FullName(),
                FlightNumber = _faker.Random.AlphaNumeric(6).ToUpper(),
                SeatNumber = $"{_faker.Random.Int(1, 30)}{_faker.Random.Char('A', 'F')}",
                Price = _faker.Finance.Amount(600, 6000, 2)
            };

            // Act
            var result = _ticketController.CreateTicketAsync(ticketRequest);
            var objectResult = (OkObjectResult)result.Result; 

            // Assert                        
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<TicketResponseDTO>(objectResult.Value);
        }
    }
}
