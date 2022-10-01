using System;
using NpgsqlTypes;

namespace Legislative.Repository
{
    public class Status
    {
        [PgName("status")] public MasterReference status { get; set; }

        [PgName("effective_date")] public DateTime effective_date { get; set; }
    }
}