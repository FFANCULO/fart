using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using Legislative.Schema;
using Legislative.Services;

namespace Legislative.Models
{
    public class LegalEventType : ObjectGraphType<LegalEvent>
    {
        public IAnalysisService Analyses { get; }

        public LegalEventType(IAnalysisService analyses)
        {
            Analyses = analyses;
            Field(o => o.Id);
            Field(o => o.Name);
            Field(o => o.Description);


            FieldAsync<LmonAnalysisType>("customer", resolve: Resolve);
            Field(o => o.Created);
            Field(o => o.Status);
        }
        async Task<object> Resolve(IResolveFieldContext<LegalEvent> context)
        {
            return await Analyses.GetCustomerByIdAsync(context.Source.CustomerId);
        }

    }
}
