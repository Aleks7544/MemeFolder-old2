namespace MemeFolder.Data.Models
{
    public interface IVisible
    {
        public bool VisibleToThePublic { get; set; }

        public bool VisibleToFollowers { get; set; }

        public bool VisibleToFriends { get; set; }

        public bool VisibleToBestFriends { get; set; }
    }
}
