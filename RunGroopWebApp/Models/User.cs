namespace RunGroopWebApp.Models
{
    public class User
    {
        public int Pace { get; set; }
        public int? Kilos { get; set; }
        public Address? Address { get; set; }
        public ICollection<Club> Clubs { get; set; }
        public ICollection<Race> Races { get; set; }
    }
}
