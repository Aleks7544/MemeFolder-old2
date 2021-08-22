namespace MemeFolder.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Visibility
    {
        [Required]
        public bool VisibleToFollowers { get; set; } = true;

        [Required]
        public bool VisibleToFriends { get; set; } = true;

        [Required]
        public bool VisibleToBestFriends { get; set; } = true;
    }
}
