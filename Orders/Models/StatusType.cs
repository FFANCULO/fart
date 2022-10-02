using GraphQL.Types;
using Legislative.Repository;

namespace Legislative.Models
{
    public class StatusType : ObjectGraphType<Status>
    {
        public StatusType()
        {
            Field(m => m.effective_date);
            Field<ObjectGraphType<MasterReferenceType>>("status", resolve: x => x.Source.status);
        }
    }
}