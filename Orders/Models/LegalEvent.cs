using System;

namespace Orders.Models
{
    public class LegalEvent
    {
        public LegalEvent(string name, string description, DateTime created, int customerId, string Id)
        {
            Name = name;
            Description = description;
            Created = created;
            CustomerId = customerId;
            this.Id = Id;
            Status = LegislationStatuses.CREATED;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; private set; }
        public int CustomerId { get; set; }
        public string Id { get; private set; }
        public LegislationStatuses Status { get; private set; }

        public void Start() 
        {
            Status = LegislationStatuses.PROCESSING;
        }
    }

    [Flags]
    public enum LegislationStatuses
    {
        CREATED = 2,
        PROCESSING = 4,
        COMPLETED = 8,
        CANCELLED = 16,
        CLOSED = 32
    }
}
