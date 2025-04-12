namespace CsAnilist.Models.User
{
    public class UserStatistics
    {
        public int count { get; set; }
        public int? minutesWatched { get; set; }
        public int? episodesWatched { get; set; }
        public int? volumesRead { get; set; }
        public int? chaptersRead { get; set; }
        public float meanScore { get; set; }
    }
}
