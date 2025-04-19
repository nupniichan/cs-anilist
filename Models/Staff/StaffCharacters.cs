using CsAnilist.Models.Character;
using CsAnilist.Models.Media;

namespace CsAnilist.Models.Staff
{
    public class StaffCharacters
    {
        public List<CharacterEdge>? edges { get; set; }
    }

    public class CharacterEdge
    {
        public string id { get; set; }
        public string role { get; set; }
        public CharacterNode node { get; set; }
    }

    public class CharacterNode
    {
        public int id { get; set; }
        public CharacterName? name { get; set; }
        public CharacterImage? image { get; set; }
        public CharacterMedia? media { get; set; }
    }

    public class CharacterName
    {
        public string? first { get; set; }
        public string? last { get; set; }
        public string? native { get; set; }
    }

    public class CharacterImage
    {
        public string? large { get; set; }
        public string? medium { get; set; }
    }

    public class CharacterMedia
    {
        public List<MediaNode>? nodes { get; set; }
    }
} 