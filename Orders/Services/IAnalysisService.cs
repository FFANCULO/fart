﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Legislative.Models;

namespace Legislative.Services
{
    public interface IAnalysisService
    {
        LmonAnalysis GetCustomerById(int id);
        Task<LmonAnalysis> GetCustomerByIdAsync(int id);
        Task<IEnumerable<LmonAnalysis>> GetCustomersAsync();
    }
}