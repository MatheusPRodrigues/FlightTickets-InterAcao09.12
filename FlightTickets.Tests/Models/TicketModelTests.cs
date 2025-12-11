using Bogus;
using FlightTickets.Models.Models;
using MongoDB.Bson;

namespace FlightTickets.Tests.Models
{
    public class TicketModelTests
    {
        private Faker _faker = new Faker("pt_BR");

        [Fact]
        public void TestandoConstrutorDeTicketComTodosOsParametros()
        {
            //Arrange - Variáveis e Objetos / Crio o necessário para criar o teste
            var passengerName = _faker.Name.FullName();
            var flightNumber = _faker.Random.AlphaNumeric(6).ToUpper();
            var seatNumber = $"{_faker.Random.Int(1, 30)}{_faker.Random.Char('A', 'F')}";
            var price = _faker.Finance.Amount(600, 6000, 2);

            //Act - ação de realizar o teste
            var ticket = new Ticket
            (
                passengerName, flightNumber, seatNumber, price
            );

            //Assert
            Assert.NotNull(ticket);
            Assert.Equal(passengerName, ticket.PassengerName);
            Assert.Equal(flightNumber, ticket.FlightNumber);
            Assert.Equal(seatNumber, ticket.SeatNumber);
            Assert.Equal(price, ticket.Price);
            Assert.True(ObjectId.TryParse(ticket.Id.ToString(), out var tempId));
        }
    }
}
