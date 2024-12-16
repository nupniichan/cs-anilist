using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAnilist.Models.Character
{
    public class CharacterName
    {
        public string first { get; set; }
        public string last { get; set; }
        public string native { get; set; }
        public List<string> alternative { get; set; }
    }
}
