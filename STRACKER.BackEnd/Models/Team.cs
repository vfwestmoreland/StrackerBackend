namespace BackEnd.Models
{
    public class Team
    {
        public int TeamId { get; set; }

        public string? TeamName { get; set; }

        public int? Wins { get; set; }

        public int? Losses { get; set; }

        public int? ParticipantId { get; set; }

        public int? EventId { get; set; }


    }
}
