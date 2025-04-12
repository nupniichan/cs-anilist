namespace CsAnilist.Models.User
{
    public class Favourites
    {
        public UserAnimeFavourites? anime { get; set; }
        public UserMangaFavourites? manga { get; set; }
        public UserCharacterFavourites? characters { get; set; }
        public UserStaffFavourites? staff { get; set; }
        public UserStudioFavourites? studios { get; set; }
    }
}
