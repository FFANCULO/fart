using System;
using GraphQL.Types;
using Legislative.Models;

namespace Legislative.Schema
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
        }
    }
}
