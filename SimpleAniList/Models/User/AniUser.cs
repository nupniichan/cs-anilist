using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAnilist.Models.User
{
    public class AniUser
    {
        public int id { get; set; }
        public string? name { get; set; }
        public UserAvatar? avatar { get; set; }
        public string? bannerImage { get; set; }
        public string? about { get; set; }
        public Favourites? favourites { get; set; }
        public UserStatisticTypes? statistics { get; set; }
        public string? siteUrl { get; set; }
    }
}
