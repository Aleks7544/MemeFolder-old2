namespace MemeFolder.Data.Models
{
    public class Visibility
    {
        public bool VisibleToFollowers { get; set; } = true;

        public bool VisibleToFriends { get; set; } = true;

        public bool VisibleToBestFriends { get; set; } = true;
    }
}
