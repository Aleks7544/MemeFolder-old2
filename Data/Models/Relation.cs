namespace MemeFolder.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Relation
    {
        [Required]
        public string FirstUserId { get; init; }

        [Required]
        public User FirstUser { get; init; }

        [Required]
        public string SecondUserId { get; init; }

        [Required]
        public User SecondUser { get; init; }

        [Required]
        public bool Follows { get; set; } = false;

        public DateTime? FollowsSince { get; set; }

        [Required]
        public bool Blocked { get; set; } = false;

        public DateTime? BlockedSince { get; set; }
    }
}
