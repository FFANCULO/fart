using System;
using System.Collections.Generic;
using System.Linq;
using Legislative.Repository;

namespace Legislative.Models
{


    public class LmonAnalysis
    {
        public LmonAnalysis(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; }
        public string Name { get; set; }

        public List<MasterReference> line_of_business { get; set; } =
            Array.Empty<MasterReference>().ToList();

        public List<MasterReference> proc_requirement { get; set; } =
            Array.Empty<MasterReference>().ToList();

        public List<MasterReference> product_type { get; set; } =
            Array.Empty<MasterReference>().ToList();

        public List<Status> status { get; set; } =
            Array.Empty<Status>().ToList();

        public Guid DxcrUuid { get; set; } = Guid.Empty;
        public string Revision { get; set; } = string.Empty;
        public DateTime DxcrInsertTimestamp { get; set; }
        public string ObjectId { get; set; } = string.Empty;
        public Guid LegalEventId { get; set; } = Guid.Empty;
        public string LegalEventRevision { get; set; } = string.Empty;
        public string RecordType { get; set; } = string.Empty;
        public DateTime? ReportDate { get; set; } = DateTime.MinValue;
        public string[] CircularLink { get; set; } = Array.Empty<string>();
        public string Comment { get; set; } = string.Empty;
        public string PersonId { get; set; } = string.Empty;
    }
}