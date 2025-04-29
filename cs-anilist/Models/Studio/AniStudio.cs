namespace CsAnilist.Models.Studio
{
    public class AniStudio
    {
        public string? name { get; set; }
        public string? siteUrl { get; set; }
        public int favourites { get; set; }
        public StudioMedia? media { get; set; }
    }
}
