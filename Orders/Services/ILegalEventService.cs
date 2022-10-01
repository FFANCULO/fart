using System.Collections.Generic;
using System.Threading.Tasks;
using Legislative.Models;

namespace Legislative.Services
{
    public interface ILegalEventService
    {
        Task<LegalEvent> GetOrderByIdAsync(string id);
        Task<IEnumerable<LegalEvent>> GetOrdersAsync();
        Task<LegalEvent> CreateAsync(LegalEvent legalEvent);
        Task<LegalEvent> StartAsync(string orderId);
    }
}
