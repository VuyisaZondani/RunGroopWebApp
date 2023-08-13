﻿using RunGroopWebApp.Data.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace RunGroopWebApp.Models
{
    public class Club
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public ClubCategory ClubCategory { get; set; }
        [ForeignKey("User")]
        public string? UserId { get; set; } 
        public User User { get; set; }

    }
}
