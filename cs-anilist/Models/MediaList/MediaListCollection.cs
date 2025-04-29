using CsAnilist.Models.Enums;
using CsAnilist.Models.User;

namespace CsAnilist.Models.MediaList
{
    public class MediaListCollection
    {
        public AniUser? user { get; set; }
        public List<MediaListGroup>? lists { get; set; }
    }

    public class MediaListGroup
    {
        public string? name { get; set; }
        public MediaListStatus? status { get; set; }
        public bool? isCustomList { get; set; }
        public bool? isSplitCompletedList { get; set; }
        public List<MediaList>? entries { get; set; }
    }
} 