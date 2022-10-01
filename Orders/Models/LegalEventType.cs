using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using Legislative.Schema;
using Legislative.Services;

namespace Legislative.Models
{
    public class LegalEventType : ObjectGraphType<LegalEvent>
    {
        public IAnalysisService AnalysisService { get; }

        public LegalEventType(IAnalysisService analysisService)
        {
            AnalysisService = analysisService;
            Field(o => o.Id);
            Field(o => o.Name);
            Field(o => o.Description);


            FieldAsync<LmonAnalysisType>("analysis", resolve: Resolve);
            Field(o => o.Created);
            Field(o => o.Status);
        }
        async Task<object> Resolve(IResolveFieldContext<LegalEvent> context)
        {
            var sourceCustomerId = context.Source.CustomerId;
            return await AnalysisService.GetAnalysisByForeignKey(sourceCustomerId);
        }

    }
}
