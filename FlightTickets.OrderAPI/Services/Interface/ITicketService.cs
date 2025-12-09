using FlightTickets.Models.Models.DTOs;

namespace FlightTickets.OrderAPI.Services.Interface
{
    public interface ITicketService
    {
        Task<TicketResponseDTO> CreateTicketAsync(TicketRequestDTO ticketRequest);
    }
}
