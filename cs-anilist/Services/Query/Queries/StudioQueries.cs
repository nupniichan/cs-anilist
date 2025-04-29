namespace CsAnilist.Services.Query.Queries
{
    public static class StudioQueries
    {
        public const string StudioSearchQuery =
        @"query ($search: String){
            Studio(search: $search) {
               name 
               siteUrl
               favourites
               media(perPage: 10, sort: POPULARITY_DESC) {
                 nodes {
                   id
                   title {
                     romaji
                     english
                     native
                   }
                   type
                   format
                   seasonYear
                   coverImage {
                     medium
                   }
                 }
               }
            }
        }";
    }
} 