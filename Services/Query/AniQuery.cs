namespace CsAnilist.Services.Query
{
    public static class AniQuery
    {
        public const string AnimeIDQuery =
        @"query ($id: Int, $type: MediaType, $asHtml: Boolean){
            Media(id: $id, type: $type) {
                id
                idMal
                title {
                    romaji
                    english
                    native
                    userPreferred
                }
                type
                format
                status
                description(asHtml: $asHtml)
                startDate {
                    year
                    month
                    day
                }
                endDate {
                    year
                    month
                    day
                }
                episodes
                duration
                chapters
                volumes
                coverImage {
                    large
                    medium
                    extraLarge
                    color
                }
                bannerImage
                averageScore
                meanScore
                popularity
                trending
                season
                seasonYear
                genres
                synonyms
                source
                hashtag
                countryOfOrigin
                isAdult
                siteUrl
                airingSchedule(page: 1, perPage: 1, notYetAired: true) {
                  nodes {
                    episode
                    timeUntilAiring
                    airingAt
                  }
                }
                trailer{
                    id
                    site
                    thumbnail
                }
                nextAiringEpisode {
                  airingAt
                  timeUntilAiring
                  episode
                }
                studios(isMain: true) {
                  edges {
                    isMain
                    node {
                      id
                      name
                      isAnimationStudio
                    }
                  }
                }
                characters(page: 1, perPage: 10, sort: ROLE) {
                  edges {
                    role
                    node {
                      id
                      name {
                        first
                        last
                        native
                        alternative
                      }
                      image {
                        large
                        medium
                      }
                      gender
                      dateOfBirth {
                        year
                        month
                        day
                      }
                      favourites
                    }
                    voiceActors(language: JAPANESE) {
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
                      language
                    }
                  }
                }
                favourites
                tags {
                  id
                  name
                  rank
                  isMediaSpoiler
                }
                relations {
                  edges {
                    relationType
                    node {
                      id
                      title {
                        romaji
                        english
                        native
                      }
                      format
                      type
                      status
                    }
                  }
                }
            }
        }";
        public const string AnimeNameQuery =
        @"query ($search: String, $type: MediaType, $asHtml: Boolean){
            Media(search: $search, type: $type) {
                id
                idMal
                title {
                    romaji
                    english
                    native
                    userPreferred
                }
                type
                format
                status
                description(asHtml: $asHtml)
                startDate {
                    year
                    month
                    day
                }
                endDate {
                    year
                    month
                    day
                }
                episodes
                duration
                chapters
                volumes
                coverImage {
                    large
                    medium
                    extraLarge
                    color
                }
                bannerImage
                averageScore
                meanScore
                popularity
                trending
                season
                seasonYear
                genres
                synonyms
                source
                hashtag
                countryOfOrigin
                isAdult
                siteUrl
                airingSchedule(page: 1, perPage: 1, notYetAired: true) {
                  nodes {
                    episode
                    timeUntilAiring
                    airingAt
                  }
                }
                trailer{
                    id
                    site
                    thumbnail
                }
                nextAiringEpisode {
                  airingAt
                  timeUntilAiring
                  episode
                }
                studios(isMain: true) {
                  edges {
                    isMain
                    node {
                      id
                      name
                      isAnimationStudio
                    }
                  }
                }
                characters(page: 1, perPage: 10, sort: ROLE) {
                  edges {
                    role
                    node {
                      id
                      name {
                        first
                        last
                        native
                        alternative
                      }
                      image {
                        large
                        medium
                      }
                      gender
                      dateOfBirth {
                        year
                        month
                        day
                      }
                      favourites
                    }
                    voiceActors(language: JAPANESE) {
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
                      language
                    }
                  }
                }
                favourites
                tags {
                  id
                  name
                  rank
                  isMediaSpoiler
                }
                relations {
                  edges {
                    relationType
                    node {
                      id
                      title {
                        romaji
                        english
                        native
                      }
                      format
                      type
                      status
                    }
                  }
                }
            }
        }";
        public const string MangaIDQuery = @"
        query ($id: Int, $type: MediaType, $asHtml: Boolean){
            Media(id: $id, type: $type) {
                id
                idMal
                title {
                    romaji
                    english
                    native
                    userPreferred
                }
                type
                format
                status
                description(asHtml: $asHtml)
                startDate {
                    year
                    month
                    day
                }
                endDate {
                    year
                    month
                    day
                }
                chapters
                volumes
                coverImage {
                    large
                    medium
                    extraLarge
                    color
                }
                bannerImage
                averageScore
                meanScore
                popularity
                trending
                season
                seasonYear
                genres
                synonyms
                source
                hashtag
                countryOfOrigin
                isAdult
                siteUrl
                studios(isMain: true) {
                  edges {
                    isMain
                    node {
                      id
                      name
                    }
                  }
                }
                characters(page: 1, perPage: 10, sort: ROLE) {
                  edges {
                    role
                    node {
                      id
                      name {
                        first
                        last
                        native
                        alternative
                      }
                      image {
                        large
                        medium
                      }
                      gender
                      dateOfBirth {
                        year
                        month
                        day
                      }
                      favourites
                    }
                  }
                }
                favourites
                tags {
                  id
                  name
                  rank
                  isMediaSpoiler
                }
                relations {
                  edges {
                    relationType
                    node {
                      id
                      title {
                        romaji
                        english
                        native
                      }
                      format
                      type
                      status
                    }
                  }
                }
            }
        }";

        public const string MangaNameQuery = @"
        query ($search: String, $type: MediaType, $asHtml: Boolean){
            Media(search: $search, type: $type) {
                id
                idMal
                title {
                    romaji
                    english
                    native
                    userPreferred
                }
                type
                format
                status
                description(asHtml: $asHtml)
                startDate {
                    year
                    month
                    day
                }
                endDate {
                    year
                    month
                    day
                }
                chapters
                volumes
                coverImage {
                    large
                    medium
                    extraLarge
                    color
                }
                bannerImage
                averageScore
                meanScore
                popularity
                trending
                season
                seasonYear
                genres
                synonyms
                source
                hashtag
                countryOfOrigin
                isAdult
                siteUrl
                studios(isMain: true) {
                  edges {
                    isMain
                    node {
                      id
                      name
                    }
                  }
                }
                characters(page: 1, perPage: 10, sort: ROLE) {
                  edges {
                    role
                    node {
                      id
                      name {
                        first
                        last
                        native
                        alternative
                      }
                      image {
                        large
                        medium
                      }
                      gender
                      dateOfBirth {
                        year
                        month
                        day
                      }
                      favourites
                    }
                  }
                }
                favourites
                tags {
                  id
                  name
                  rank
                  isMediaSpoiler
                }
                relations {
                  edges {
                    relationType
                    node {
                      id
                      title {
                        romaji
                        english
                        native
                      }
                      format
                      type
                      status
                    }
                  }
                }
            }
        }";
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
        public const string UserSearchQuery =
        @"query($name: String, $asHtml: Boolean){
            User(name: $name){
            id
            name
            avatar {
                large
                medium
            }
            bannerImage
            about (asHtml: $asHtml)
            favourites {
                anime {
                    nodes {
                        id
                        title {
                            romaji
                            english
                            native
                            userPreferred
                        }
                        siteUrl
                    }
                }
                manga {
                    nodes {
                        id
                        title {
                            romaji
                            english
                            native
                            userPreferred
                        }
                        siteUrl
                    }
                }
                characters {
                    nodes {
                        id
                        name {
                            first
                            last
                        }
                        siteUrl
                        image {
                            large
                        }
                    }
                }
                staff {
                    nodes {
                        name {
                            first
                            last
                            native
                        }
                        siteUrl
                    }
                }
                studios {
                    nodes {
                        name
                        siteUrl
                    }
                }
            }
            statistics {
                anime {
                    count
                    minutesWatched
                    episodesWatched
                    meanScore
                    statuses {
                        status
                        count
                        meanScore
                        chaptersRead
                    }
                }
                manga {
                    count
                    volumesRead
                    chaptersRead
                    meanScore
                    statuses {
                        status
                        count
                        meanScore
                        chaptersRead
                    }
                }
            }
            siteUrl
            }
        }";
    }
}
