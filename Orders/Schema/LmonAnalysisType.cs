using System;
using System.Collections.Generic;
using System.Text;

using GraphQL.Types;
using Orders.Models;

namespace Orders.Schema
{
    public class LmonAnalysisType : ObjectGraphType<LmonAnalysis>
    {
        public LmonAnalysisType()
        {
            Field(c => c.Id);
            Field(c => c.Name);
        }
    }
}
