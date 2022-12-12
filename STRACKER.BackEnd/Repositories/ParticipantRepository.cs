using BackEnd.Models;
using System.Data.SqlClient;

namespace BackEnd.Repositories
{
    public class ParticipantRepository : IParticipantRepository
    {
        private readonly IConfiguration _configuration;

        public ParticipantRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection Connection => new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

        public List<Participant> GetParticipantList()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @" SELECT participantId,
                                                userId,
                                                teamId,
                                                firstName,
                                                lastName,
                                                jerseyNumber,
                                                statId
                                         FROM participant
                                        ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Participant> participants = new List<Participant>();
                        while (reader.Read())
                        {
                            Participant participant = new Participant()
                            {
                                ParticipantId = reader.GetInt32(reader.GetOrdinal("id")),
                                UserId = reader[(reader.GetOrdinal("userId"))] == DBNull.Value ? null : reader.GetInt32(reader.GetOrdinal("userId")),
                                TeamId = reader[(reader.GetOrdinal("teamId"))] == DBNull.Value ? null : reader.GetInt32(reader.GetOrdinal("teamId")),
                                FirstName = reader[(reader.GetOrdinal("firstName"))] == DBNull.Value ? null : reader.GetString(reader.GetOrdinal("firstName")),
                                LastName = reader[(reader.GetOrdinal("lastName"))] == DBNull.Value ? null : reader.GetString(reader.GetOrdinal("lastName")),
                                JerseyNumber = reader[(reader.GetOrdinal("jerseyNumber"))] == DBNull.Value ? null : reader.GetInt32(reader.GetOrdinal("jerseyNumber")),
                                StatId = reader[(reader.GetOrdinal("teamId"))] == DBNull.Value ? null : reader.GetInt32(reader.GetOrdinal("teamId")),
                            };

                            participants.Add(participant);
                        }

                        return participants;
                    }

                }
            }
        }
    }
}

