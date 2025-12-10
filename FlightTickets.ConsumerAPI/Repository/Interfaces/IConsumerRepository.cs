using FlightTickets.Models.Models;

namespace FlightTickets.ConsumerAPI.Repository.Interfaces
{
    public interface IConsumerRepository
    {
        Task SaveApprovedTicketsAsync(Ticket ticket);
        Task SaveDeniedTicketsAsync(Ticket ticket);
    }
}
