using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Legislative.Models;
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

    public async IAsyncEnumerable<LegalEvent> GetLegalEventsAsync()
    {
        await using var connection =
            new NpgsqlConnection(
                "Host=leavemealone-1.cmjnz9otzdc3.us-east-1.rds.amazonaws.com;Database=Graph2;Username=postgres;Password=L0c0m0tiv?");

        await connection.OpenAsync();

        connection.TypeMapper.MapComposite<MasterReference>("MasterReference", new MasterReference1Translator());

        connection.TypeMapper.MapComposite<Status>("Status", new MasterReference2Translator());


        var sql =
            "SELECT dxcr_uuid, revision, \"dxcr_insertTimestamp\", " +
            "legal_type, description, effective_date, jurisdiction" +
            "\r\n\tFROM dxcr.legal_event;";

        await using var command =
            new NpgsqlCommand(sql, connection);


        var dataTable = new DataTable();
        new NpgsqlDataAdapter(command).Fill(dataTable);
        foreach (DataRow dataTableRow in dataTable.Rows)
        {
            var legalEvent = new LegalEvent("", "", DateTime.Now, 1, Guid.NewGuid().ToString());
            legalEvent.DxcrUuid = Guid.TryParse(dataTableRow["dxcr_uuid"].ToString(), out var r1) ? r1 : Guid.Empty;
            legalEvent.Revision = dataTableRow["revision"].ToString() ?? "";
            legalEvent.DxcrInsertTimestamp =
                DateTime.TryParse(dataTableRow["dxcr_insertTimestamp"].ToString(), out var r2) ? r2 : DateTime.MinValue;
            legalEvent.LegalType = dataTableRow["legal_type"].ToString();
            legalEvent.Description = dataTableRow["description"].ToString();
            legalEvent.EffectiveDate = DateTime.TryParse(dataTableRow["effective_date"].ToString(), out var r3)
                ? r3
                : DateTime.MinValue;
            legalEvent.jurisdiction = (MasterReference[])dataTableRow["jurisdiction"];
            yield return legalEvent;
        }

       
    }
}