using BackEnd.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BackEnd.Repositories
{
    public class StatsRepository : IStatsReposistory
    { 
        private readonly IConfiguration _configuration;

        public StatsRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection Connection => new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

        public List<Stats> GetAllStats()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @" SELECT statId,
                                                goals,
                                                assist,
                                                saves,
                                                participantId,
                                                teamId,
                                                eventId
                                         From stats
                                        ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Stats> stats = new List<Stats>();
                        while (reader.Read())
                        {
                            Stats stat = new Stats()
                            {
                                StatId = reader[(reader.GetOrdinal("statId"))] == DBNull.Value ? null : reader.GetInt32(reader.GetOrdinal("statId")),
                                Goals = reader[(reader.GetOrdinal("goals"))] == DBNull.Value ? null : reader.GetInt32(reader.GetOrdinal("goals")),
                                Assist = reader[(reader.GetOrdinal("assist"))] == DBNull.Value ? null : reader.GetInt32(reader.GetOrdinal("assist")),
                                Saves = reader[(reader.GetOrdinal("saves"))] == DBNull.Value ? null : reader.GetInt32(reader.GetOrdinal("saves")),
                                ParticipantId = reader[(reader.GetOrdinal("participantId"))] == DBNull.Value ? null : reader.GetInt32(reader.GetOrdinal("participantId")),
                                TeamId = reader[(reader.GetOrdinal("teamId"))] == DBNull.Value ? null : reader.GetInt32(reader.GetOrdinal("teamId")),
                                EventId = reader[(reader.GetOrdinal("eventId"))] == DBNull.Value ? null : reader.GetInt32(reader.GetOrdinal("eventId")),

                            };

                            stats.Add(stat);
                        }

                        return stats;
                    }

                }
            }
        }

    }
}


