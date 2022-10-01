using System.Collections.Generic;
using Orders.Models;
using System.Threading.Tasks;

namespace Orders.Services
{
    public interface ILegalEventService
    {
        Task<LegalEvent> GetOrderByIdAsync(string id);
        Task<IEnumerable<LegalEvent>> GetOrdersAsync();
        Task<LegalEvent> CreateAsync(LegalEvent legalEvent);
        Task<LegalEvent> StartAsync(string orderId);
    }
}
