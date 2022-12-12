using BackEnd.Models;

namespace BackEnd.Repositories
{
    public interface IUserRepository
    {
        List<User> GetUsers();
    }
}
