using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAnilist.Services.Query
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
                chapters
                volumes
                coverImage {
                    large
                    medium
                }
                bannerImage
                averageScore
                meanScore
                season
                genres
                source
                siteUrl
                duration
                airingSchedule(page: 1, perPage: 1, notYetAired: true) {
                  nodes {
                    episode
                    timeUntilAiring
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
                chapters
                volumes
                coverImage {
                    large
                    medium
                }
                bannerImage
                averageScore
                meanScore
                season
                genres
                source
                siteUrl
                duration
                airingSchedule(page: 1, perPage: 1, notYetAired: true) {
                  nodes {
                    episode
                    timeUntilAiring
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
                }
                bannerImage
                averageScore
                meanScore
                season
                genres
                source
                siteUrl
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
                }
                bannerImage
                averageScore
                meanScore
                season
                genres
                source
                siteUrl
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
    		  primaryOccupations
    		  gender
    		 homeTown
            }
        }";
        public const string StudioSearchQuery =
        @"query ($search: String){
            Studio(search: $search) {
               name 
               siteUrl
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
