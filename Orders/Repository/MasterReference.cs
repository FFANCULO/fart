using NpgsqlTypes;

namespace Legislative.Repository
{
    public class MasterReference
    {
        [PgName("refCode")] public string refCode { get; set; }

        [PgName("refDescription")] public string refDescription { get; set; }
    }
}