using CsAnilist.Models.Enums;
using CsAnilist.Models.Media;
using CsAnilist.Models.User;

namespace CsAnilist.Models.MediaList
{
    public class MediaList
    {
        public int id { get; set; }
        public int userId { get; set; }
        public string? userName { get; set; }
        public MediaType? type { get; set; }
        public MediaListStatus? status { get; set; }
        public int mediaId { get; set; }
        public bool? isFollowing { get; set; }
        public string? notes { get; set; }
        public MediaDate? startedAt { get; set; }
        public MediaDate? completedAt { get; set; }
        public int? score { get; set; }
        public int? progress { get; set; }
        public int? progressVolumes { get; set; }
        public int? repeat { get; set; }
        public int? priority { get; set; }
        public bool? Private { get; set; }
        public bool? hiddenFromStatusLists { get; set; }
        public AniMedia? media { get; set; }
        public AniUser? user { get; set; }
    }
} 