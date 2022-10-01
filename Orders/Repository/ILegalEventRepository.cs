using System.Threading.Tasks;

namespace Legislative.Repository
{
    public interface ILegalEventRepository
    {
        Task<object> GetLegalEventsAsync();
    }
}