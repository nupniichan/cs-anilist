using CsAnilist.Models.Character;
using CsAnilist.Models.Media;
using CsAnilist.Models.MediaList;
using CsAnilist.Models.Staff;
using CsAnilist.Models.Studio;
using CsAnilist.Models.User;

namespace CsAnilist.Models
{
    public class Page
    {
        public PageInfo? pageInfo { get; set; }
        public List<AniMedia>? media { get; set; }
        public List<AniCharacter>? characters { get; set; }
        public List<AniStaff>? staff { get; set; }
        public List<AniStudio>? studios { get; set; }
        public List<AniUser>? users { get; set; }
        public List<MediaTrend>? mediaTrends { get; set; }
        public List<MediaList.MediaList>? mediaList { get; set; }
    }

    public class PageInfo
    {
        public int total { get; set; }
        public int perPage { get; set; }
        public int currentPage { get; set; }
        public int lastPage { get; set; }
        public bool hasNextPage { get; set; }
    }
} 