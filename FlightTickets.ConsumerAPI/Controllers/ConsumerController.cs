using FlightTickets.ConsumerAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlightTickets.ConsumerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumerController : ControllerBase
    {
        private readonly ILogger<ConsumerController> _logger;
        private readonly IConsumerService _consumerService;

        public ConsumerController
        (
            ILogger<ConsumerController> logger,
            IConsumerService consumerService
        )
        {
            _logger = logger;
            _consumerService = consumerService;
        }

        [HttpPost]
        public async Task<IActionResult> TicketSaveDatabase()
        {
            try
            {
                _logger.LogInformation("Saving queues in mongo database");
                await _consumerService.GetTicketsFromQueuesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurring while persisit data in Database: {ex.Message}");
                return Problem();
            }
        }
    }
}
