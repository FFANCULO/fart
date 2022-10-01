using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Legislative.Models;
using Legislative.Repository.Translator;
using Npgsql;

namespace Legislative.Repository
{
    public class LmonAnalysisRepository : ILmonAnalysisRepository
    {
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
                yield return new LmonAnalysis(index, $"fucked{index}");
            }
        }
    }
}