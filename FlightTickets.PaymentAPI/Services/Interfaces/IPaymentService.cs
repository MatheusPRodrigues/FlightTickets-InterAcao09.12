namespace FlightTickets.PaymentAPI.Services.Interfaces
{
    public interface IPaymentService
    {
        Task GetTicketFromQueueAsync();
    }
}
