using System;
using System.Collections.Generic;
using Legislative.Models;

namespace Legislative.Repository
{
  
    public interface ILmonAnalysisRepository
    {
        IAsyncEnumerable<LmonAnalysis> GetAnalysisByIdAsync(Guid sourceDxcrUuid);
    }
}