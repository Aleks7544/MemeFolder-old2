namespace MemeFolder.Services.Shared
{
    public class VisibilityFormModel
    {
        public bool VisibleToThePublic { get; set; }

        public bool VisibleToFollowers { get; set; }

        public bool VisibleToFriends { get; set; }

        public bool VisibleToBestFriends { get; set; }
    }
}
