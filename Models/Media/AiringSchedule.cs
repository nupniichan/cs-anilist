namespace CsAnilist.Models.Media
{
    public class AiringSchedule
    {
        public List<AiringScheduleNode> nodes { get; set; }
    }
    public class AiringScheduleNode
    {
        public int episode { get; set; }
        public int timeUntilAiring { get; set; }
    }
}
