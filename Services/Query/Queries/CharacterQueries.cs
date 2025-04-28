namespace CsAnilist.Services.Query.Queries
{
    public static class CharacterQueries
    {
        public const string CharacterSearchQuery =
        @"query ($search: String, $asHtml: Boolean){
              Character(search: $search) {
                  name {
                      first
                      last
                      native
                      alternative
                  }
                  description(asHtml: $asHtml)
                  image {
                      large
                      medium
                  }
                  dateOfBirth {
    				  year
    				  month
    				  day
    			  }
                  gender
                  siteUrl
                  favourites
              }
        }";
    }
} 