using FlightTickets.Models.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FlightTickets.ConsumerAPI.Data
{
    public class ConnectionDB
    {
        private readonly IMongoCollection<Ticket> _ticketApprovedCollection;
        private readonly IMongoCollection<Ticket> _ticketDeniedCollection;

        public ConnectionDB(IOptions<MongoDBSettings> mongoDBSettings)
        {
            IMongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _ticketApprovedCollection = database.GetCollection<Ticket>(mongoDBSettings.Value.TicketApprovedCollection);
            _ticketDeniedCollection = database.GetCollection<Ticket>(mongoDBSettings.Value.TicketDeniedCollection);
        }

        public IMongoCollection<Ticket> GetApprovedCollection()
        {
            return _ticketApprovedCollection;
        }

        public IMongoCollection<Ticket> GetDeniedCollection()
        {
            return _ticketDeniedCollection;
        }
    }
}
