using Legislative.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Legislative.Repository
{
    public interface ILegalEventRepository
    {
        IAsyncEnumerable<LegalEvent> GetLegalEventsAsync();
    }
}