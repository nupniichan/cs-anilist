using CsAnilist.Models.Enums;

namespace CsAnilist.Models.Media
{
    public class AniMedia
    {
        public int id { get; set; }
        public int? idMal { get; set; }
        public Title? title { get; set; }
        public MediaType? type { get; set; }
        public MediaFormat? format { get; set; }
        public MediaStatus? status { get; set; }
        public string? description { get; set; }
        public StartDate? startDate { get; set; }
        public EndDate? endDate { get; set; }
        public int? episodes { get; set; }
        public int? chapters { get; set; }
        public int? volumes { get; set; }
        public CoverImage? coverImage { get; set; }
        public string? bannerImage { get; set; }
        public int? averageScore { get; set; }
        public int? meanScore { get; set; }
        public int? popularity { get; set; }
        public int? trending { get; set; }
        public MediaSeason? season { get; set; }
        public int? seasonYear { get; set; }
        public List<string>? genres { get; set; }
        public List<string>? synonyms { get; set; }
        public MediaSource? source { get; set; }
        public string? hashtag { get; set; }
        public string? countryOfOrigin { get; set; }
        public bool? isAdult { get; set; }
        public string? siteUrl { get; set; }
        public int? duration { get; set; }
        public AiringSchedule? airingSchedule { get; set; }
        public NextAiringEpisode? nextAiringEpisode { get; set; }
        public MediaStudio? studios { get; set; }
        public MediaCharacters? characters { get; set; }
        public int? favourites { get; set; }
        public List<MediaTag>? tags { get; set; }
        public MediaRelation? relations { get; set; }
        public MediaTrailer? trailer { get; set; }
    }
}
