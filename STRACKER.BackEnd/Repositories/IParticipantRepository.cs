using BackEnd.Models;

namespace BackEnd.Repositories
{
    public interface IParticipantRepository
    {
        List<Participant> GetParticipantList();
    }
}
