using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Legislative.Models;
using Legislative.Repository.Translator;
using Legislative.Utility;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Legislative.Repository;

public class LegalEventRepository : ILegalEventRepository
{
    public PostgresSettings Settings { get; }

    public LegalEventRepository(PostgresSettings settings)
    {
        Settings = settings;
    }

    public async IAsyncEnumerable<LegalEvent> GetLegalEventsAsync()
    {
        await using var connection =
            new NpgsqlConnection(Settings.ConnectionString);

        await connection.OpenAsync();

        connection.TypeMapper.MapComposite<MasterReference>("MasterReference", new MasterReference1Translator());

        connection.TypeMapper.MapComposite<Status>("Status", new MasterReference2Translator());


        var sql =
            "SELECT dxcr_uuid, revision, \"dxcr_insertTimestamp\", " +
            "legal_type, description, effective_date, jurisdiction" +
            "\r\n\tFROM dxcr.legal_event;";

        await using var command =
            new NpgsqlCommand(sql, connection);

        var dataReader = await command.ExecuteReaderAsync(CommandBehavior.Default);

        while (await dataReader.ReadAsync())
        {
            var legalEvent = new LegalEvent("", "", DateTime.Now, 1, Guid.NewGuid().ToString());
            legalEvent.DxcrUuid = Guid.TryParse(dataReader["dxcr_uuid"].ToString(), out var r1) ? r1 : Guid.Empty;
            legalEvent.Revision = dataReader["revision"].ToString() ?? "";
            legalEvent.DxcrInsertTimestamp =
                DateTime.TryParse(dataReader["dxcr_insertTimestamp"].ToString(), out var r2) ? r2 : DateTime.MinValue;
            legalEvent.LegalType = dataReader["legal_type"].ToString();
            legalEvent.Description = dataReader["description"].ToString();
            legalEvent.EffectiveDate = DateTime.TryParse(dataReader["effective_date"].ToString(), out var r3)
                ? r3
                : DateTime.MinValue;
            legalEvent.jurisdiction = (MasterReference[])dataReader["jurisdiction"];
            yield return legalEvent;

        }
    }
}