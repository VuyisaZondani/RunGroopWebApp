﻿namespace RunGroopWebApp.ViewModels
{
    public class EditUserDashboardViewModel
    {
        public string Id { get; set; }
        public int? Pace { get; set; }
        public int? Kilos { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public IFormFile Image { get; set; }
    }
}
