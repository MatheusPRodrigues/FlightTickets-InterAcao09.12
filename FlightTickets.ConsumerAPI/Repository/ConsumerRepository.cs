using FlightTickets.ConsumerAPI.Data;
using FlightTickets.ConsumerAPI.Repository.Interfaces;
using FlightTickets.Models.Models;
using MongoDB.Driver;

namespace FlightTickets.ConsumerAPI.Repository
{
    public class ConsumerRepository : IConsumerRepository
    {
        private readonly ILogger<ConsumerRepository> _logger;
        private readonly IMongoCollection<Ticket> _approvedCollection;
        private readonly IMongoCollection<Ticket> _deniedCollection;

        public ConsumerRepository
        (
            ILogger<ConsumerRepository> logger,
            ConnectionDB connectionDB
        )
        {
            _logger = logger;
            _approvedCollection = connectionDB.GetApprovedCollection();
            _deniedCollection = connectionDB.GetDeniedCollection();
        }

        public async Task SaveApprovedTicketsAsync(Ticket ticket)
        {
            try
            {
                await _approvedCollection.InsertOneAsync(ticket);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurring while persist data in approvalsCollection " + ex.Message);
                throw;
            }
        }

        public async Task SaveDeniedTicketsAsync(Ticket ticket)
        {
            try
            {
                await _deniedCollection.InsertOneAsync(ticket);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurring while persist data in deniedsCollection " + ex.Message);

            }
        }
    }
}
