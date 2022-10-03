using System.ComponentModel.DataAnnotations;

namespace Legislative.Utility
{
    public class PostgresSettings
    {
        [Required]
        public string ConnectionString { get; set; } = "Not Assigned";
    }
}
