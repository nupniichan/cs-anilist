using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace SimpleAnilist.Models.Character
{
    public class AniCharacter
    {
        public CharacterName? name { get; set; }
        public string? description { get; set; }
        public CharacterImage? image { get; set; }
        public CharacterDateOfBirth? dateOfBirth { get; set; }
        public string? gender { get; set; }
        public string? siteUrl { get; set; }
        public int? favourites { get; set; }
    }
}
