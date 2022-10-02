using GraphQL.Types;
using Legislative.Repository;

namespace Legislative.Models
{
    public class MasterReferenceType : ObjectGraphType<MasterReference>
    {
        public MasterReferenceType()
        {
            Field(m => m.refCode);
            Field(m => m.refDescription);
        }
    }
}
