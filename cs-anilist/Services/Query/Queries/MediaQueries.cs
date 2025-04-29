namespace CsAnilist.Services.Query.Queries
{
    public static class MediaQueries
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

        public const string MediaTrendQuery =
        @"query ($mediaId: Int!) {
            Media(id: $mediaId) {
                id
                title {
                    romaji
                    english
                    native
                    userPreferred
                }
                format
                status
                description
                episodes
                duration
                chapters
                volumes
                coverImage {
                    large
                    medium
                }
                bannerImage
                averageScore
                meanScore
                popularity
                trending
                favourites
                siteUrl
                nextAiringEpisode {
                    airingAt
                    timeUntilAiring
                    episode
                }
            }
        }";

        public const string AllMediaTrendsQuery =
        @"query ($page: Int, $perPage: Int, $type: MediaType) {
            Page(page: $page, perPage: $perPage) {
                pageInfo {
                    total
                    perPage
                    currentPage
                    lastPage
                    hasNextPage
                }
                media(sort: TRENDING_DESC, type: $type) {
                    id
                    title {
                        romaji
                        english
                        native
                        userPreferred
                    }
                    type
                    format
                    status
                    episodes
                    chapters
                    volumes
                    coverImage {
                        large
                        medium
                    }
                    averageScore
                    popularity
                    trending
                    favourites
                    season
                    seasonYear
                    nextAiringEpisode {
                        airingAt
                        timeUntilAiring
                        episode
                    }
                }
            }
        }";
    }
} 