using System;

namespace Orders.Models
{
    public class LegislationEvent
    {
        public LegislationEvent(string orderId, string name, LegislationStatuses status, DateTime timestamp)
        {
            OrderId = orderId;
            Name = name;
            Status = status;
            Timestamp = timestamp;
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string OrderId { get; set; }
        public string Name { get; set; }
        public LegislationStatuses Status { get; set; }
        public DateTime Timestamp { get; private set; }
    }
}
