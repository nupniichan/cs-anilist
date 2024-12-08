using SimpleAnilist.AnilistAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAnilist.Models.User
{
    public class UserFavourites
    {
        public UserAnimeFavourites? anime { get; set; }
        public UserMangaFavourites? manga { get; set; }
        public UserCharacterFavourites? characters { get; set; }
        public UserStaffFavourites? staff { get; set; }
        public UserStudioFavourites? studios { get; set; }
    }
}
