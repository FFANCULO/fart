using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Legislative.Models;

namespace Legislative.Services;

/// <inheritdoc />
public class AnalysisService : IAnalysisService
{
    private readonly IList<LmonAnalysis> _customers;

    public AnalysisService()
    {
        _customers = new List<LmonAnalysis>();
        _customers.Add(new LmonAnalysis(1, "KinetEco"));
        _customers.Add(new LmonAnalysis(2, "Pixelford Photography"));
        _customers.Add(new LmonAnalysis(3, "Topsy Turvy"));
        _customers.Add(new LmonAnalysis(4, "Leaf & Mortar"));
    }

    public LmonAnalysis GetCustomerById(int id)
    {
        return GetCustomerByIdAsync(id).Result;
    }

    public Task<LmonAnalysis> GetCustomerByIdAsync(int id)
    {
        return Task.FromResult(_customers.Single(o => Equals(o.Id, id)));
    }

    public Task<IEnumerable<LmonAnalysis>> GetCustomersAsync()
    {
        return Task.FromResult(_customers.AsEnumerable());
    }
}