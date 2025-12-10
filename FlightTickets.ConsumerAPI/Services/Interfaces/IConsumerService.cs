using FlightTickets.Models.Models;

namespace FlightTickets.ConsumerAPI.Services.Interfaces
{
    public interface IConsumerService
    {
        Task GetTicketsFromQueuesAsync();
        Task SaveApprovedTicketsToCollectionAsync(Ticket ticket);
        Task SaveDeniedTicketsToCollectionAsync(Ticket ticket);
    }
}
