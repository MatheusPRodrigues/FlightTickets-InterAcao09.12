using FlightTickets.PaymentAPI.Services;

namespace FlightTickets.Tests.Services.PaymentServiceTests
{
    public class PaymentServiceTests
    {
        private PaymentService _paymentService = new PaymentService();

        [Fact]
        public void Teste()
        {
            var result = _paymentService.GetTicketFromQueueAsync();
            var resultWithNotException = Record.ExceptionAsync(() => result).Result;

            Assert.NotNull(result);
            Assert.Null(resultWithNotException);
        }
    }
}
