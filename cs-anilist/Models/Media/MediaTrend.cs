namespace CsAnilist.Models.Media
{
    public class MediaTrend
    {
        public int id { get; set; }
        public int mediaId { get; set; }
        public int date { get; set; }
        public int? trending { get; set; }
        public int? averageScore { get; set; }
        public int? popularity { get; set; }
        public int? episode { get; set; }
        public bool? releasing { get; set; }
        public AniMedia? media { get; set; }
    }
} 