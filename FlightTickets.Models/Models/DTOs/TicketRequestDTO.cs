namespace FlightTickets.Models.Models.DTOs
{
    public class TicketRequestDTO
    {
        public string PassengerName { get; init; }
        public string FlightNumber { get; init; }
        public string SeatNumber { get; init; }
        public decimal Price { get; init; }
    }
}
