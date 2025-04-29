namespace CsAnilist.Services.Query.Queries
{
    public static class UserQueries
    {
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

        public const string UserMediaListQuery =
        @"query ($userId: Int, $userName: String, $type: MediaType, $status: MediaListStatus) {
            MediaListCollection(userId: $userId, userName: $userName, type: $type, status: $status) {
                user {
                    id
                    name
                    avatar {
                        large
                        medium
                    }
                    bannerImage
                }
                lists {
                    name
                    status
                    isCustomList
                    isSplitCompletedList
                    entries {
                        id
                        mediaId
                        status
                        score
                        progress
                        progressVolumes
                        repeat
                        priority
                        private
                        startedAt {
                            year
                            month
                            day
                        }
                        completedAt {
                            year
                            month
                            day
                        }
                        updatedAt
                        createdAt
                        notes
                        media {
                            id
                            title {
                                romaji
                                english
                                native
                                userPreferred
                            }
                            coverImage {
                                large
                                medium
                            }
                            type
                            format
                            status
                            episodes
                            chapters
                            volumes
                            averageScore
                            popularity
                        }
                    }
                }
            }
        }";
    }
} 