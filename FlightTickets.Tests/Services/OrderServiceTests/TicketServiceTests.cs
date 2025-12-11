using Bogus;
using FlightTickets.Models.Models.DTOs;
using FlightTickets.OrderAPI.Services;

namespace FlightTickets.Tests.Services.OrderServiceTests
{
    public class TicketServiceTests
    {
        private readonly Faker _faker = new Faker("pt_BR");
        private readonly TicketService _ticketService;

        public TicketServiceTests()
        {
            _ticketService = new TicketService();
        }

        [Fact]
        public void TicketServiceCreateTicketReturn()
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
            var result = _ticketService.CreateTicketAsync(ticketRequest);

            // Assert
            Assert.IsType<TicketResponseDTO>(result.Result);
        }
    }
}
