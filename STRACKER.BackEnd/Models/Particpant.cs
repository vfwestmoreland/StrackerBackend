using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models
{
    public class Participant
    {
        public int? ParticipantId { get; set; }  

        public int? UserId { get; set; }

        public int? TeamId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public int? JerseyNumber { get; set; }

        public int? StatId  { get; set; }

    }
}
