namespace CsAnilist.Models.Media
{
    public class MediaStudio
    {
        public List<StudioEdge> edges { get; set; }
    }

    public class StudioEdge
    {
        public bool isMain { get; set; }
        public StudioNode node { get; set; }
    }

    public class StudioNode
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool isAnimationStudio { get; set; }
    }
} 