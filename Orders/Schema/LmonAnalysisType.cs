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
        }
    }
}
