﻿namespace RunGroopWebApp.ViewModels
{
    public class UserDetailViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public int? Pace { get; set; }
        public int? Kilos { get; set; }
        public string? ProfileImageUrl { get; set; }
        public IFormFile Image { get; set; }
    }
}
