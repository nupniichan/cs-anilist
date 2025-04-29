using CsAnilist.Models.Enums;

namespace CsAnilist.Models.Media
{
    public class MediaRelation
    {
        public List<MediaEdge> edges { get; set; }
    }

    public class MediaEdge
    {
        public Enums.MediaRelation relationType { get; set; }
        public MediaNode node { get; set; }
    }

    public class MediaNode
    {
        public int id { get; set; }
        public Title title { get; set; }
        public MediaFormat? format { get; set; }
        public MediaType type { get; set; }
        public MediaStatus status { get; set; }
    }
} 