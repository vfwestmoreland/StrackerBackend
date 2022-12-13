﻿using System.Diagnostics.Contracts;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models
{
    public class User
    {
        public int? Id { get; set; }

        public string? FirebaseUserId { get; set; }

        public string? Email { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public int? UserTypeId { get; set; }

        public UserType? UserType { get; set; }

        public Boolean? IsParticipant { get; set; }

    }
}