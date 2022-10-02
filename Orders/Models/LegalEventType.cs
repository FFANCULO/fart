using System.Linq;
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
            Field(o => o.Created);
            Field(o => o.Status);

            FieldAsync<LmonAnalysisType>("analysis", resolve: Resolve);
            Field(o => o.Description);

            // tbd            Field<ListGraphType<NonNullGraphType<StringGraphType>>>("imagesList", resolve: x => x.Source?.ImagesList)
            Field<ListGraphType<MasterReferenceType>>("jurisdictions", resolve: x => x.Source?.jurisdiction.ToList().AsReadOnly());


        }

        

        async Task<object> Resolve(IResolveFieldContext<LegalEvent> context)
        {
            var sourceCustomerId = context.Source.CustomerId;
            return await AnalysisService.GetAnalysisByForeignKey(sourceCustomerId);
        }

    }
}
