using GraphQL.Types;
using Orders.Services;

namespace Orders.Schema
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