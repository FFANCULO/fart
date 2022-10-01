using GraphQL.Types;
using Legislative.Models;
using Legislative.Services;

namespace Legislative.Schema
{
    public class LegislationQuery : ObjectGraphType<object>
    {
        public LegislationQuery(ILegalEventService legalEvents)
        {
            Name = "Query";

            FieldAsync<ListGraphType<LegalEventType>>("orders", resolve: async context => await legalEvents.GetOrdersAsync());
        }
    }
}