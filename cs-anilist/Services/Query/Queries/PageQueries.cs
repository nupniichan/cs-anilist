namespace CsAnilist.Services.Query.Queries
{
    public static class PageQueries
    {
        public const string PagedMediaQuery =
        @"query ($page: Int, $perPage: Int, $type: MediaType, $search: String, $sort: [MediaSort], $asHtml: Boolean) {
            Page(page: $page, perPage: $perPage) {
                pageInfo {
                    total
                    perPage
                    currentPage
                    lastPage
                    hasNextPage
                }
                media(type: $type, search: $search, sort: $sort) {
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
                    isAdult
                    countryOfOrigin
                    siteUrl
                }
            }
        }";

        public const string PagedCharacterQuery =
        @"query ($page: Int, $perPage: Int, $search: String, $sort: [CharacterSort], $asHtml: Boolean) {
            Page(page: $page, perPage: $perPage) {
                pageInfo {
                    total
                    perPage
                    currentPage
                    lastPage
                    hasNextPage
                }
                characters(search: $search, sort: $sort) {
                    id
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
                    age
                    bloodType
                    siteUrl
                    favourites
                    media(perPage: 5, sort: POPULARITY_DESC) {
                        edges {
                            node {
                                id
                                title {
                                    romaji
                                    english
                                    native
                                    userPreferred
                                }
                                type
                                format
                                siteUrl
                                coverImage {
                                    medium
                                }
                            }
                            characterRole
                        }
                    }
                }
            }
        }";

        public const string PagedStaffQuery =
        @"query ($page: Int, $perPage: Int, $search: String, $sort: [StaffSort], $asHtml: Boolean) {
            Page(page: $page, perPage: $perPage) {
                pageInfo {
                    total
                    perPage
                    currentPage
                    lastPage
                    hasNextPage
                }
                staff(search: $search, sort: $sort) {
                    id
                    name {
                        first
                        last
                        native
                        alternative
                    }
                    languageV2
                    image {
                        large
                        medium
                    }
                    description(asHtml: $asHtml)
                    primaryOccupations
                    gender
                    dateOfBirth {
                        year
                        month
                        day
                    }
                    dateOfDeath {
                        year
                        month
                        day
                    }
                    age
                    yearsActive
                    homeTown
                    siteUrl
                    favourites
                    staffMedia(perPage: 5, sort: POPULARITY_DESC) {
                        edges {
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
                            staffRole
                        }
                    }
                }
            }
        }";

        public const string PagedStudioQuery =
        @"query ($page: Int, $perPage: Int, $search: String, $sort: [StudioSort]) {
            Page(page: $page, perPage: $perPage) {
                pageInfo {
                    total
                    perPage
                    currentPage
                    lastPage
                    hasNextPage
                }
                studios(search: $search, sort: $sort) {
                    id
                    name
                    isAnimationStudio
                    siteUrl
                    favourites
                    media(perPage: 5, sort: POPULARITY_DESC) {
                        nodes {
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
                            startDate {
                                year
                            }
                        }
                    }
                }
            }
        }";
    }
} 