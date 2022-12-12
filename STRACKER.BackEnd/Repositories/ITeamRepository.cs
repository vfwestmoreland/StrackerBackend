using BackEnd.Models;

namespace BackEnd.Repositories
{
    public interface ITeamRepository
    {
        List<Team> GetTeamList();
    }
}
