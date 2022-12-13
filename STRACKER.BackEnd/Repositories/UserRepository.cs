using BackEnd.Models;
using Microsoft.Data.SqlClient;
using System.Data;

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
                                Id = reader[(reader.GetOrdinal("userId"))] == DBNull.Value ? null : reader.GetInt32(reader.GetOrdinal("userId")),
                                FirebaseUserId = reader[(reader.GetOrdinal("firebaseUserId"))] == DBNull.Value ? null : reader.GetString(reader.GetOrdinal("firebaseUserId")),
                                Email = reader[(reader.GetOrdinal("email"))] == DBNull.Value ? null : reader.GetString(reader.GetOrdinal("email")),
                                FirstName = reader[(reader.GetOrdinal("firstName"))] == DBNull.Value ? null : reader.GetString(reader.GetOrdinal("firstName")),
                                LastName = reader[(reader.GetOrdinal("lastName"))] == DBNull.Value ? null : reader.GetString(reader.GetOrdinal("lastName")),
                                UserTypeId = reader[(reader.GetOrdinal("userTypeId"))] == DBNull.Value ? null : reader.GetInt32(reader.GetOrdinal("userTypeId")),
                                UserType = new UserType()
                                {
                                    UserTypeId = reader[(reader.GetOrdinal("userTypeId"))] == DBNull.Value ? null : reader.GetInt32(reader.GetOrdinal("userTypeId")),
                                    UserTypeName = reader[(reader.GetOrdinal("userTypeName"))] == DBNull.Value ? null : reader.GetString(reader.GetOrdinal("userTypeName")),
                                },
                                IsParticipant = reader[(reader.GetOrdinal("isParticipant"))] == DBNull.Value ? null : reader.GetBoolean(reader.GetOrdinal("isParticipant")),
                            };
                            users.Add(user);
                        }
                        return users;
                    }
                }
            }
        }

        public User GetUserByFirebaseId(string firebaseUserId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT  u.userId,
                                                u.firebaseUserId,
                                                u.email,
                                                u.firstName,
                                                u.lastName,
                                                ut.userTypeId,
                                                ut.userTypeName,
                                                u.isParticipant
                                        FROM users u
                                        LEFT JOIN userType ut ON ut.userTypeId = u.userTypeId
                                        WHERE u.firebaseUserId = @FirebaseUserId
					  ";


                    cmd.Parameters.AddWithValue("@FirebaseUserId", firebaseUserId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            User user = new User()
                            {
                                Id = reader[(reader.GetOrdinal("userId"))] == DBNull.Value ? null : reader.GetInt32(reader.GetOrdinal("userId")),
                                FirebaseUserId = reader[(reader.GetOrdinal("firebaseUserId"))] == DBNull.Value ? null : reader.GetString(reader.GetOrdinal("firebaseUserId")),
                                Email = reader[(reader.GetOrdinal("email"))] == DBNull.Value ? null : reader.GetString(reader.GetOrdinal("email")),
                                FirstName = reader[(reader.GetOrdinal("firstName"))] == DBNull.Value ? null : reader.GetString(reader.GetOrdinal("firstName")),
                                LastName = reader[(reader.GetOrdinal("lastName"))] == DBNull.Value ? null : reader.GetString(reader.GetOrdinal("lastName")),
                                UserTypeId = reader[(reader.GetOrdinal("userTypeId"))] == DBNull.Value ? null : reader.GetInt32(reader.GetOrdinal("userTypeId")),
                                UserType = new UserType()
                                {
                                    UserTypeId = reader[(reader.GetOrdinal("userTypeId"))] == DBNull.Value ? null : reader.GetInt32(reader.GetOrdinal("userTypeId")),
                                    UserTypeName = reader[(reader.GetOrdinal("userTypeName"))] == DBNull.Value ? null : reader.GetString(reader.GetOrdinal("userTypeName")),
                                },
                                IsParticipant = reader[(reader.GetOrdinal("isParticipant"))] == DBNull.Value ? null : reader.GetBoolean(reader.GetOrdinal("isParticipant")),

                            };
                            return user;
                        }
                        else
                        {
                            return null;
                        }
                    }

                }
            }
        }

        public void AddUser(User user)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
									INSERT INTO users (firebaseUserId, email, firstName, lastName, userTypeId, isParticipant) 
									OUTPUT INSERTED.userId
									VALUES (@firebaseUserId, @email, @firstName, @lastName, @userTypeId, @isParticipant)
									";

                    cmd.Parameters.AddWithValue("@firebaseUserId", user.FirebaseUserId);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@firstName", user.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", user.LastName);
                    cmd.Parameters.AddWithValue("@userTypeId", user.UserTypeId);
                    cmd.Parameters.AddWithValue("@isParticipant", user.IsParticipant);


                    int id = (int)cmd.ExecuteScalar();

                    user.Id = id;
                }
            }

        }
    }
}