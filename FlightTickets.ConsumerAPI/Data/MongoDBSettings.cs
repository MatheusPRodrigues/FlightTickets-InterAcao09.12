namespace FlightTickets.ConsumerAPI.Data
{
    public class MongoDBSettings
    {
        public string ConnectionURI { get; set; } = null;
        public string DatabaseName { get; set; } = null;
        public string TicketApprovedCollection { get; set; } = null;
        public string TicketDeniedCollection { get; set; } = null;
    }
}
