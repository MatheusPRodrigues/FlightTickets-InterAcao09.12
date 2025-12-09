using FlightTickets.PaymentAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightTickets.PaymentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly IPaymentService _paymentService;

        public PaymentController(
            ILogger<PaymentController> logger,
            IPaymentService paymentService
        )
        {
            _logger = logger;
            _paymentService = paymentService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                _logger.LogInformation("Consume queue 'ticketOrder'.");
                await _paymentService.GetTicketFromQueueAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in queue 'ticketOrder' processing");
                return Problem(ex.Message);
            }
        }
    }
}
