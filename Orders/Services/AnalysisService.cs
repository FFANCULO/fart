using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Legislative.Models;
using Legislative.Repository;

namespace Legislative.Services;

/// <inheritdoc />
public class AnalysisService : IAnalysisService
{
    public ILmonAnalysisRepository Repository { get; }
    private readonly IList<LmonAnalysis> _customers = new List<LmonAnalysis>();

    public AnalysisService(ILmonAnalysisRepository repository)
    {
        Repository = repository;
        _customers.Add(new LmonAnalysis(1, "KinetEco"));
        _customers.Add(new LmonAnalysis(2, "Pixelford Photography"));
        _customers.Add(new LmonAnalysis(3, "Topsy Turvy"));
        _customers.Add(new LmonAnalysis(4, "Leaf & Mortar"));
    }

    public LmonAnalysis GetCustomerById(int id)
    {
        return GetAnalysisByForeignKey(id).Result;
    }

    public Task<LmonAnalysis> GetAnalysisByForeignKey(int id)
    {
      // TBD  Repository.GetAnalysisByIdAsync()
        bool Predicate(LmonAnalysis o)
        {
            return Equals(o.Id, id);
        }

        return Task.FromResult(_customers.Single(Predicate));
    }

    public Task<IEnumerable<LmonAnalysis>> GetCustomersAsync()
    {
        return Task.FromResult(_customers.AsEnumerable());
    }
}