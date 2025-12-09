using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FlightTickets.Models.Models.DTOs
{
    public class TicketResponseDTO
    {
        [BsonId]
        public string Id { get; init; }
        public string PassengerName { get; init; }
        public string FlightNumber { get; init; }
        public string SeatNumber { get; init; }
        public decimal Price { get; init; }
    }
}
