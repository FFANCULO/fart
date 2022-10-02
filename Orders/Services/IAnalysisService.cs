using System.Collections.Generic;
using System.Threading.Tasks;
using Legislative.Models;

namespace Legislative.Services
{
    public interface IAnalysisService
    {
        LmonAnalysis GetCustomerById(int id);
        IAsyncEnumerable<LmonAnalysis> GetAnalysisByForeignKey(int id);
        Task<IEnumerable<LmonAnalysis>> GetCustomersAsync();
    }
}
