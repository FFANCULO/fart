using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using Legislative.Services;

namespace Legislative.Models;

public class LegalEventType : ObjectGraphType<LegalEvent>
{
    public LegalEventType(IAnalysisService analysisService)
    {
        AnalysisService = analysisService;
        Field(o => o.Id);
        Field(o => o.Name);
        Field(o => o.Created);
        Field(o => o.Status);

        FieldAsync<ListGraphType<LmonAnalysisType>>("analysis", resolve: Resolve);
        Field(o => o.Description);

        Field<ListGraphType<MasterReferenceType>>("jurisdictions",
            resolve: x => x.Source?.jurisdiction.ToList().AsReadOnly());
    }

    public IAnalysisService AnalysisService { get; }

    private async Task<object> Resolve(IResolveFieldContext<LegalEvent> context)
    {
        var legislationId = context.Source.LegislationId;
        var list = new List<LmonAnalysis>();
        await foreach (var analysis in AnalysisService.GetAnalysisByForeignKey(legislationId)) list.Add(analysis);
        return list;
    }
}