using Microsoft.Data.SqlClient;
using BackEnd.Models;

namespace BackEnd.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly IConfiguration _configuration;

        public EventRepository(IConfiguration configuration)
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

        public List<Event> GetEventList()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select  e.eventId,
                                                e.teamId,
                                                e.userId,
                                                e.participantId,
                                                e.statId,
                                                e.eventName,
                                                e.startDate,
                                                e.endDate,
                                                e.eventYear,
                                                e.eventTime,
                                                et.eventTypeId,
                                                et.eventTypeName,
                                                o.oneOfFourSeasonsId,
                                                o.seasonName
                                         FROM event e
                                         LEFT JOIN eventType et ON et.eventTypeId = e.eventTypeId
                                         LEFT JOIN oneOfFourSeasons o ON o.oneOfFourSeasonsId = e.oneOfFourSeasonsId
                                         ";
                    
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Event> events = new List<Event>();
                        while (reader.Read())
                        {
                            Event eventInfo = new Event()
                            {
                                EventId = reader.GetInt32(reader.GetOrdinal("eventId")),
                                TeamId = reader[(reader.GetOrdinal("teamId"))] == DBNull.Value ? null : reader.GetInt32(reader.GetOrdinal("teamId")),
                                UserId = reader[(reader.GetOrdinal("userId"))] == DBNull.Value ? null : reader.GetInt32(reader.GetOrdinal("userId")),
                                ParticipantId = reader[(reader.GetOrdinal("participantId"))] == DBNull.Value ? null : reader.GetInt32(reader.GetOrdinal("participantId")),
                                StatId = reader[(reader.GetOrdinal("statId"))] == DBNull.Value ? null : reader.GetInt32(reader.GetOrdinal("statId")),
                                EventName = reader[(reader.GetOrdinal("eventName"))] == DBNull.Value ? null : reader.GetString(reader.GetOrdinal("eventName")),
                                StartDate = reader[(reader.GetOrdinal("startDate"))] == DBNull.Value ? null : reader.GetDateTime(reader.GetOrdinal("startDate")),
                                EndDate = reader[(reader.GetOrdinal("endDate"))] == DBNull.Value ? null : reader.GetDateTime(reader.GetOrdinal("endDate")),
                                EventYear = reader[(reader.GetOrdinal("eventYear"))] == DBNull.Value ? null : reader.GetDateTime(reader.GetOrdinal("eventYear")),
                                EventTime = reader[(reader.GetOrdinal("eventTime"))] == DBNull.Value ? null : reader.GetDateTime(reader.GetOrdinal("eventTime")),
                                EventTypeId = reader[(reader.GetOrdinal("eventTypeId"))] == DBNull.Value ? null : reader.GetInt32(reader.GetOrdinal("eventTypeId")),
                                EventType = new EventType()
                                {
                                    EventTypeId = reader[(reader.GetOrdinal("eventTypeId"))] == DBNull.Value ? null : reader.GetInt32(reader.GetOrdinal("eventTypeId")),
                                    EventTypeName = reader[(reader.GetOrdinal("eventTypeName"))] == DBNull.Value ? null : reader.GetString(reader.GetOrdinal("eventTypeName")),
                                },
                                OneOfFourSeasonsId = reader[(reader.GetOrdinal("oneOfFourSeasonsId"))] == DBNull.Value ? null : reader.GetInt32(reader.GetOrdinal("oneOfFourSeasonsId")),
                                OneOfFourSeasons = new OneOfFourSeasons()
                                {
                                    OneOfFourSeasonsId = reader[(reader.GetOrdinal("oneOfFourSeasonsId"))] == DBNull.Value ? null : reader.GetInt32(reader.GetOrdinal("oneOfFourSeasonsId")),
                                    SeasonName = reader[(reader.GetOrdinal("seasonName"))] == DBNull.Value ? null : reader.GetString(reader.GetOrdinal("seasonName")),
                                },
                            };
                            events.Add(eventInfo);
                        }
                        return events;
                    }

                }
            }
        }
    }
}
