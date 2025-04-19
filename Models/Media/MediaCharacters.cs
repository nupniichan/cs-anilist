using CsAnilist.Models.Character;

namespace CsAnilist.Models.Media
{
    public class MediaCharacters
    {
        public List<CharacterEdge>? edges { get; set; }
    }

    public class CharacterEdge
    {
        public string role { get; set; }
        public CharacterNode node { get; set; }
        public List<VoiceActor>? voiceActors { get; set; }
    }

    public class CharacterNode
    {
        public int id { get; set; }
        public CharacterName name { get; set; }
        public CharacterImage image { get; set; }
        public string gender { get; set; }
        public CharacterDateOfBirth dateOfBirth { get; set; }
        public int favourites { get; set; }
    }

    public class CharacterName
    {
        public string first { get; set; }
        public string last { get; set; }
        public string native { get; set; }
        public List<string> alternative { get; set; }
    }

    public class CharacterImage
    {
        public string large { get; set; }
        public string medium { get; set; }
    }

    public class CharacterDateOfBirth
    {
        public int? year { get; set; }
        public int? month { get; set; }
        public int? day { get; set; }
    }

    public class VoiceActor
    {
        public int id { get; set; }
        public VoiceActorName name { get; set; }
        public VoiceActorImage image { get; set; }
        public string language { get; set; }
    }

    public class VoiceActorName
    {
        public string first { get; set; }
        public string last { get; set; }
        public string native { get; set; }
    }

    public class VoiceActorImage
    {
        public string large { get; set; }
        public string medium { get; set; }
    }
} 