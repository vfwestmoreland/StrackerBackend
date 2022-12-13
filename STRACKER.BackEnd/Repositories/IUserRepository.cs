using BackEnd.Models;

namespace BackEnd.Repositories
{
    public interface IUserRepository
    {
        List<User> GetUsers();

        User GetUserByFirebaseId(string firebaseUserId);

        public void AddUser(User user);
    }
}
