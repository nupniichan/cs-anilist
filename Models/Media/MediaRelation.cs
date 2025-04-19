namespace CsAnilist.Models.Media
{
    public class MediaRelation
    {
        public List<MediaEdge> edges { get; set; }
    }

    public class MediaEdge
    {
        public string relationType { get; set; }
        public MediaNode node { get; set; }
    }

    public class MediaNode
    {
        public int id { get; set; }
        public Title title { get; set; }
        public string format { get; set; }
        public string type { get; set; }
        public string status { get; set; }
    }
} 