using GraphQL.Types;
using Legislative.Models;

namespace Legislative.Schema
{
    public class LegislationEventType : ObjectGraphType<LegislationEvent>
    {
        public LegislationEventType()
        {
            Field(e => e.Id);
            Field(e => e.Name);
            Field(e => e.OrderId);
            Field(e => e.Status);
            Field(e => e.Timestamp);
        }
    }
}
