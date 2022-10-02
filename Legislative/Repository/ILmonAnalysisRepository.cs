using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Legislative.Models;

namespace Legislative.Repository
{
  
    public interface ILmonAnalysisRepository
    {
        Task<object?> GetAnalysisAsync();
        IAsyncEnumerable<LmonAnalysis> GetAnalysisByIdAsync(Guid sourceDxcrUuid);
    }
}