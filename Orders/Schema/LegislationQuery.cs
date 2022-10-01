using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using Legislative.Models;
using Legislative.Services;

namespace Legislative.Schema
{
    public class LegislationQuery : ObjectGraphType<object>
    {
        public ILegalEventService LegalEvents { get; }

        public LegislationQuery(ILegalEventService legalEvents)
        {
            LegalEvents = legalEvents;
            Name = "Query";

  
            FieldAsync<ListGraphType<LegalEventType>>("orders", resolve: Resolve);
        }

        async Task<object> Resolve(IResolveFieldContext<object> context)
        {
            return await LegalEvents.GetOrdersAsync();
        }

    }
}