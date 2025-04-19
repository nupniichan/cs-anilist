namespace CsAnilist.Models.Studio
{
    public class StudioMedia
    {
        public List<StudioMediaNode>? nodes { get; set; }
    }

    public class StudioMediaNode
    {
        public int id { get; set; }
        public StudioMediaTitle? title { get; set; }
        public string? type { get; set; }
        public string? format { get; set; }
        public int? seasonYear { get; set; }
        public StudioMediaCoverImage? coverImage { get; set; }
    }

    public class StudioMediaTitle
    {
        public string? romaji { get; set; }
        public string? english { get; set; }
        public string? native { get; set; }
    }

    public class StudioMediaCoverImage
    {
        public string? medium { get; set; }
    }
} 