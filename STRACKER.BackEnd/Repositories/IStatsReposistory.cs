using BackEnd.Models;

namespace BackEnd.Repositories
{
    public interface IStatsReposistory
    {
        List<Stats> GetAllStats();
    }
}
