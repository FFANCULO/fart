using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Legislative.Models;
using Legislative.Repository.Translator;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Legislative.Repository
{
    public class LmonAnalysisRepository : ILmonAnalysisRepository
    {
        public IConfiguration Configuration { get; }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public LmonAnalysisRepository(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        async Task<object> ILmonAnalysisRepository.GetAnalysisAsync()
        {
            var dataTable = await ExecuteQuery();
            return dataTable;
        }

        internal async Task<DataTable> ExecuteQuery()
        {
            await using var connection =
                new NpgsqlConnection(
                    "Host=leavemealone-1.cmjnz9otzdc3.us-east-1.rds.amazonaws.com;Database=Graph2;Username=postgres;Password=L0c0m0tiv?");

            await connection.OpenAsync();

            connection.TypeMapper.MapComposite<MasterReference>("MasterReference", new MasterReference1Translator());

            connection.TypeMapper.MapComposite<Status>("Status", new MasterReference2Translator());


            var sql = "SELECT dxcr_uuid, revision, \"dxcr_insertTimestamp\", " +
                      "object_id, legal_event_id, legal_event_revision, record_type, " +
                      "report_date, circular_link, line_of_business, proc_requirement, " +
                      "product_type, status, comment, person_id\r\n\tFROM dxcr.lmon_analysis";
               

            await using NpgsqlCommand command =
                new NpgsqlCommand(sql, connection);


            using var dataTable = new DataTable();
            new NpgsqlDataAdapter(command).Fill(dataTable);

            return dataTable;
        }

        public async IAsyncEnumerable<LmonAnalysis> GetAnalysisByIdAsync(Guid sourceDxcrUuid)
        {
            var dataTable = await ExecuteQuery();
            for (var index = 0; index < dataTable.Rows.Count; index++)
            {
                var dataTableRow = dataTable.Rows[index];
                var analysis = new LmonAnalysis(index, $"foo{index}");
                analysis.DxcrUuid = Guid.TryParse(dataTableRow["dxcr_uuid"].ToString(), out var r1) ? r1 : Guid.Empty;
                analysis.Revision = dataTableRow["revision"].ToString();
                analysis.DxcrInsertTimestamp = DateTime.TryParse(dataTableRow["dxcr_insertTimestamp"].ToString(), out var r2) ? r2 : DateTime.MinValue;
                analysis.ObjectId = dataTableRow["object_id"].ToString();
                analysis.LegalEventId = Guid.TryParse(dataTableRow["legal_event_id"].ToString(), out var r3) ? r3 : Guid.Empty;
                analysis.LegalEventRevision = dataTableRow["legal_event_revision"].ToString() ?? "";
                analysis.RecordType = dataTableRow["record_type"].ToString() ?? "";
                analysis.ReportDate = DateTime.TryParse(dataTableRow["report_date"].ToString(), out var r4) ? r4 : DateTime.MinValue;
                analysis.CircularLink = (dataTableRow["circular_link"] as string[]) ?? Array.Empty<string>();
                analysis.line_of_business = (dataTableRow["line_of_business"] as MasterReference[] ?? Array.Empty<MasterReference>()).ToList();
                analysis.proc_requirement = (dataTableRow["proc_requirement"] as MasterReference[] ?? Array.Empty<MasterReference>()).ToList();
                analysis.product_type = (dataTableRow["product_type"] as MasterReference[] ?? Array.Empty<MasterReference>()).ToList();
                analysis.status = (dataTableRow["status"] as Status[] ?? Array.Empty<Status>()).ToList();
                analysis.Comment = dataTableRow["comment"].ToString();
                analysis.PersonId = dataTableRow["person_id"].ToString();


                yield return analysis;
            }
        }
    }
}