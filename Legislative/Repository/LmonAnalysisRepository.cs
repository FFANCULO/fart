using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Legislative.Models;
using Legislative.Repository.Translator;
using Legislative.Utility;
using Npgsql;

namespace Legislative.Repository
{
    public class LmonAnalysisRepository : ILmonAnalysisRepository
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public LmonAnalysisRepository(PostgresSettings settings)
        {
            Settings = settings;
        }

        public PostgresSettings Settings { get; }

        public async IAsyncEnumerable<LmonAnalysis> GetAnalysisByIdAsync(Guid sourceDxcrUuid)
        {
            await using var connection =
                new NpgsqlConnection(Settings.ConnectionString);

            await connection.OpenAsync();

            connection.TypeMapper.MapComposite<MasterReference>("MasterReference", new MasterReference1Translator());

            connection.TypeMapper.MapComposite<Status>("Status", new MasterReference2Translator());

            var sql = "SELECT dxcr_uuid, revision, \"dxcr_insertTimestamp\", " +
                      "object_id, legal_event_id, legal_event_revision, record_type, " +
                      "report_date, circular_link, line_of_business, proc_requirement, " +
                      "product_type, status, comment, person_id\r\n\tFROM dxcr.lmon_analysis";

            await using var command =
                new NpgsqlCommand(sql, connection);

            var dataReader = await command.ExecuteReaderAsync(CommandBehavior.Default);

            var index = 0;
            while (await dataReader.ReadAsync())
            {
                var analysis = new LmonAnalysis(index++, $"foo{index}");
                analysis.DxcrUuid = Guid.TryParse(dataReader["dxcr_uuid"].ToString(), out var r1) ? r1 : Guid.Empty;
                analysis.Revision = dataReader["revision"].ToString();
                analysis.DxcrInsertTimestamp =
                    DateTime.TryParse(dataReader["dxcr_insertTimestamp"].ToString(), out var r2) ? r2 : DateTime.MinValue;
                analysis.ObjectId = dataReader["object_id"].ToString();
                analysis.LegalEventId =
                    Guid.TryParse(dataReader["legal_event_id"].ToString(), out var r3) ? r3 : Guid.Empty;
                analysis.LegalEventRevision = dataReader["legal_event_revision"].ToString() ?? "";
                analysis.RecordType = dataReader["record_type"].ToString() ?? "";
                analysis.ReportDate = DateTime.TryParse(dataReader["report_date"].ToString(), out var r4)
                    ? r4
                    : DateTime.MinValue;
                analysis.CircularLink = dataReader["circular_link"] as string[] ?? Array.Empty<string>();
                analysis.line_of_business =
                    (dataReader["line_of_business"] as MasterReference[] ?? Array.Empty<MasterReference>()).ToList();
                analysis.proc_requirement =
                    (dataReader["proc_requirement"] as MasterReference[] ?? Array.Empty<MasterReference>()).ToList();
                analysis.product_type =
                    (dataReader["product_type"] as MasterReference[] ?? Array.Empty<MasterReference>()).ToList();
                analysis.status = (dataReader["status"] as Status[] ?? Array.Empty<Status>()).ToList();
                analysis.Comment = dataReader["comment"].ToString();
                analysis.PersonId = dataReader["person_id"].ToString();

                yield return analysis;
            }
        }
    }
}