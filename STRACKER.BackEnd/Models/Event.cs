namespace BackEnd.Models

{
    public class Event
    {
        public int EventId { get; set; }

        public int? TeamId { get; set; }

        public int? UserId { get; set; }

        public int? ParticipantId { get; set; }

        public int? StatId { get; set; }

        public string? EventName { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? EventYear { get; set; }  

        public DateTime? EventTime { get; set; }

        public int? EventTypeId { get; set; }

        public EventType? EventType { get; set; }

        public int? OneOfFourSeasonsId { get; set; }
        public OneOfFourSeasons? OneOfFourSeasons { get; set; }


    }
}
