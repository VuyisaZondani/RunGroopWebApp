﻿using Microsoft.AspNetCore.Identity;
using RunGroopWebApp.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RunGroopWebApp.Models
{
    public class User : IdentityUser
    {    
        public int? Pace { get; set; }
        public int? Kilos { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? City { get; set; }
        public Provinces Provinces { get; set; }
        public string? Province { get; set; }
        //Links addesses
        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public Address? Address { get; set; }
        public ICollection<Club> Clubs { get; set; }
        public ICollection<Race> Races { get; set; }
    }
}
