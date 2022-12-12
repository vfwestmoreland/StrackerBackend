using BackEnd.Models;

namespace BackEnd.Repositories
{
    public interface IEventRepository
    {
        List<Event> GetEventList();
    }
}
