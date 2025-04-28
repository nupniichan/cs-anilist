using CsAnilist.Services.Query.Queries;

namespace CsAnilist.Services.Query
{
    public static class AniQuery
    {
        // Media Queries
        public const string AnimeIDQuery = MediaQueries.AnimeIDQuery;
        public const string AnimeNameQuery = MediaQueries.AnimeNameQuery;
        public const string MangaIDQuery = MediaQueries.MangaIDQuery;
        public const string MangaNameQuery = MediaQueries.MangaNameQuery;
        public const string MediaTrendQuery = MediaQueries.MediaTrendQuery;
        public const string AllMediaTrendsQuery = MediaQueries.AllMediaTrendsQuery;

        // Character Queries
        public const string CharacterSearchQuery = CharacterQueries.CharacterSearchQuery;

        // Staff Queries
        public const string StaffSearchQuery = StaffQueries.StaffSearchQuery;

        // Studio Queries
        public const string StudioSearchQuery = StudioQueries.StudioSearchQuery;

        // User Queries
        public const string UserSearchQuery = UserQueries.UserSearchQuery;
        public const string UserMediaListQuery = UserQueries.UserMediaListQuery;

        // Page Queries
        public const string PagedMediaQuery = PageQueries.PagedMediaQuery;
        public const string PagedCharacterQuery = PageQueries.PagedCharacterQuery;
        public const string PagedStaffQuery = PageQueries.PagedStaffQuery;
        public const string PagedStudioQuery = PageQueries.PagedStudioQuery;
    }
}
