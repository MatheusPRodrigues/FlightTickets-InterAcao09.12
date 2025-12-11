using FlightTickets.PaymentAPI.Controllers;
using FlightTickets.PaymentAPI.Services;
using FlightTickets.PaymentAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FlightTickets.Tests.Controllers.PaymentControllerTests
{
    public class PaymentControllerTests
    {
        private ILogger<PaymentController> _logger;
        private IPaymentService _paymentService;
        private PaymentController _paymentController;

        public PaymentControllerTests()
        {
            _logger = new LoggerFactory().CreateLogger<PaymentController>();
            _paymentService = new PaymentService();
            _paymentController = new PaymentController(_logger, _paymentService);
        }

        [Fact]
        [Trait("VerbosHTTP", "Get")]
        public void ProcessPaymentMustReturnOkResult()
        {
            // Act 
            var result = _paymentController.Get().Result;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
    }
}
