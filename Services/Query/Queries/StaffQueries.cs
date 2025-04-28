namespace CsAnilist.Services.Query.Queries
{
    public static class StaffQueries
    {
        public const string StaffSearchQuery =
        @"query ($search: String, $asHtml: Boolean){
            Staff(search: $search) {
              name {
                first
                last
                native
              }
              languageV2
              image {
                 large
                 medium
              }
              description(asHtml: $asHtml)
              siteUrl
    		  gender
    		  homeTown
              favourites
              characters(page: 1, perPage: 10, sort: FAVOURITES_DESC) {
                edges {
                  id
                  role
                  node {
                    id
                    name {
                      first
                      last
                      native
                    }
                    image {
                      large
                      medium
                    }
                    media(perPage: 1) {
                      nodes {
                        id
                        title {
                          romaji
                          english
                          native
                        }
                        type
                        format
                      }
                    }
                  }
                }
              }
              staffMedia(page: 1, perPage: 10, sort: POPULARITY_DESC) {
                edges {
                  id
                  staffRole
                  node {
                    id
                    title {
                      romaji
                      english
                      native
                    }
                    type
                    format
                    coverImage {
                      medium
                    }
                  }
                }
              }
            }
        }";
    }
} 