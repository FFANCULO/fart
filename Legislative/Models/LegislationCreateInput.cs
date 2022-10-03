using System;

namespace Legislative.Models
{
    public class LegislationCreateInput
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int LegislationId { get; set; }
        public DateTime Created { get; set; }
    }
}
