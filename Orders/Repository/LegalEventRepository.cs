using System.Data;
using System.Threading.Tasks;
using Legislative.Repository.Translator;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Legislative.Repository;

public class LegalEventRepository : ILegalEventRepository
{
    public LegalEventRepository(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public async Task<object> GetLegalEventsAsync()
    {
        await using var connection =
            new NpgsqlConnection(
                "Host=leavemealone-1.cmjnz9otzdc3.us-east-1.rds.amazonaws.com;Database=Graph2;Username=postgres;Password=L0c0m0tiv?");

        await connection.OpenAsync();

        connection.TypeMapper.MapComposite<MasterReference>("MasterReference", new MasterReference1Translator());

        connection.TypeMapper.MapComposite<Status>("Status", new MasterReference2Translator());


        var sql = "SELECT dxcr_uuid, revision, \"dxcr_insertTimestamp\", object_id, " +
                  " legal_event_id, legal_event_revision, record_type, report_date, circular_link, " +
                  " line_of_business, proc_requirement, product_type, status, comment, person_id " +
                  " FROM dxcr.lmon_analysis;";

        await using var command =
            new NpgsqlCommand(sql, connection);


        var dataTable = new DataTable();
        new NpgsqlDataAdapter(command).Fill(dataTable);

        return dataTable;
    }
}