using BackEnd.Models;
using Microsoft.Data.SqlClient;

namespace BackEnd.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            }

        }
        public List<User> GetUsers()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT  u.userId,
                                                u.firebaseUserId,
                                                u.userName,
                                                u.email,
                                                u.firstName,
                                                u.lastName,
                                                u.userTypeId,
                                                ut.userTypeName,
                                                u.isParticipant
                                        FROM users u
                                        LEFT JOIN userType ut ON ut.userTypeId = u.userTypeId
                                       ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<User> users = new List<User>();
                        while (reader.Read())
                        {
                            User user = new User()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("userId")),
                                FirebaseUserId = reader[(reader.GetOrdinal("firebaseUserId"))] == DBNull.Value ? null : reader.GetString(reader.GetOrdinal("firebaseUserId")),
                                Email = reader[(reader.GetOrdinal("email"))] == DBNull.Value ? null : reader.GetString(reader.GetOrdinal("email")),
                                FirstName = reader[(reader.GetOrdinal("firstName"))] == DBNull.Value ? null : reader.GetString(reader.GetOrdinal("firstName")),
                                LastName = reader[(reader.GetOrdinal("lastName"))] == DBNull.Value ? null : reader.GetString(reader.GetOrdinal("lastName")),
                                UserTypeId = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                                UserType = new UserType()
                                {
                                    UserTypeId = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                                    UserTypeName = reader.GetString(reader.GetOrdinal("userTypeName"))
                                },
                                IsParticipant = reader.GetBoolean(reader.GetOrdinal("isParticipant")),
                            };
                            users.Add(user);
                        }
                        return users;
                    }
                }
            }
        }
    }
}