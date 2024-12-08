using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAnilist.Models.User
{
    public class UserMediaStatus
    {
        public string status { get; set; }
        public int count { get; set; }
        public float? meanScore { get; set; }
        public int? chaptersRead { get; set; }
    }
}
