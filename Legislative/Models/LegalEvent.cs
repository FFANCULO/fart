﻿using System;
using Legislative.Repository;

namespace Legislative.Models
{
    
    public class LegalEvent
    {
        public LegalEvent(string name, string description, DateTime created, int legislationId, string Id)
        {
            Name = name;
            Description = description;
            Created = created;
            LegislationId = legislationId;
            this.Id = Id;
            Status = LegislationStatuses.CREATED;
        }
        public string Name { get; set; }
        public DateTime Created { get; private set; }
        public int LegislationId { get; set; }
        public string Id { get; private set; }
        public LegislationStatuses Status { get; private set; }
        public void Start()
        {
            Status = LegislationStatuses.PROCESSING;
        }

        /// <summary>
        /// 
        /// </summary>
        public MasterReference[] jurisdiction { get; set; } = Array.Empty<MasterReference>();

        public Guid DxcrUuid { get; set; } = Guid.Empty;
        public string Revision { get; set; } = String.Empty;
        public DateTime DxcrInsertTimestamp { get; set; } = DateTime.MinValue;
        public string LegalType { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public DateTime EffectiveDate { get; set; } = DateTime.MinValue;
    }

    [Flags]
    public enum LegislationStatuses
    {
        CREATED = 2,
        PROCESSING = 4,
    }
}
