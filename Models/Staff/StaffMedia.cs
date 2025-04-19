namespace CsAnilist.Models.Staff
{
    public class StaffMedia
    {
        public List<StaffMediaEdge>? edges { get; set; }
    }

    public class StaffMediaEdge
    {
        public string id { get; set; }
        public string staffRole { get; set; }
        public StaffMediaNode node { get; set; }
    }

    public class StaffMediaNode
    {
        public int id { get; set; }
        public StaffMediaTitle? title { get; set; }
        public string? type { get; set; }
        public string? format { get; set; }
        public StaffMediaCoverImage? coverImage { get; set; }
    }

    public class StaffMediaTitle
    {
        public string? romaji { get; set; }
        public string? english { get; set; }
        public string? native { get; set; }
    }

    public class StaffMediaCoverImage
    {
        public string? medium { get; set; }
    }
} 