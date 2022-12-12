using BackEnd.Models;
using Microsoft.Data.SqlClient;


namespace BackEnd.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly IConfiguration _configuration;

        public TeamRepository(IConfiguration configuration)
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

        public List<Team> GetTeamList()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT  t.teamId,
                                                t.participantId,
                                                t.eventId,
                                                t.teamName,
                                                t.wins,
                                                t.losses
                                      FROM team t
						  ";

                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Team> teams = new List<Team>();
                        while (reader.Read())
                        {
                            Team team = new Team()
                            {
                                TeamId = reader.GetInt32(reader.GetOrdinal("teamId")),
                                ParticipantId = reader[(reader.GetOrdinal("participantId"))] == DBNull.Value ? null : reader.GetInt32(reader.GetOrdinal("participantId")),
                                EventId = reader[(reader.GetOrdinal("eventId"))] == DBNull.Value ? null : reader.GetInt32(reader.GetOrdinal("eventId")),
                                TeamName = reader[(reader.GetOrdinal("teamName"))] == DBNull.Value ? null : reader.GetString(reader.GetOrdinal("teamName")),
                                Wins = reader[(reader.GetOrdinal("wins"))] == DBNull.Value ? null : reader.GetInt32(reader.GetOrdinal("wins")),
                                Losses = reader[(reader.GetOrdinal("losses"))] == DBNull.Value ? null : reader.GetInt32(reader.GetOrdinal("losses")),
                            };
                            teams.Add(team);
                        }
                        return teams;
                    }
                }
            }
        }
    }
}
