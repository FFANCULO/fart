﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GraphQL.Types;
using Legislative.Repository;

namespace Legislative.Models
{
    public class LmonAnalysisType : ObjectGraphType<LmonAnalysis>
    {

        public LmonAnalysisType()
        {
            Field(c => c.Id);
            Field(c => c.Name);
            Field(c => c.DxcrUuid);
            Field(c => c.Revision);
            Field(c => c.DxcrInsertTimestamp);
            Field(c => c.ObjectId);
            Field(c => c.LegalEventId);
            Field(c => c.LegalEventRevision);
            Field(c => c.RecordType);
            //    Field(c => c.ReportDate);
            Field(c => c.CircularLink);
            Field(c => c.Comment);
            Field(c => c.PersonId);

            // tbd  -- product_type
            Field<ListGraphType<MasterReferenceType>>("products",
                resolve: x => x.Source?.product_type.ToList().AsReadOnly() ?? new ReadOnlyCollection<MasterReference>(new List<MasterReference>()));
            Field<ListGraphType<MasterReferenceType>>("lobs",
                resolve: x => x.Source?.line_of_business.ToList().AsReadOnly() ?? new ReadOnlyCollection<MasterReference>(new List<MasterReference>()));
            Field<ListGraphType<MasterReferenceType>>("requirements",
                resolve: x => x.Source?.proc_requirement.ToList().AsReadOnly() ?? new ReadOnlyCollection<MasterReference>(new List<MasterReference>()));
        }
    }
}
