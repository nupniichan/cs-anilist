using CsAnilist.Models;
using CsAnilist.Models.Character;
using CsAnilist.Models.Media;
using CsAnilist.Models.MediaList;
using CsAnilist.Models.Staff;
using CsAnilist.Models.Studio;
using CsAnilist.Models.User;
using CsAnilist.Services.Query;
using CsAnilist.Models.Enums;

namespace CsAnilist.Services
{
    public class CsAniListService
    {
        private readonly GraphQLAnilist _apiClient;

        public CsAniListService()
        {
            _apiClient = new GraphQLAnilist();
        }

        public async Task<AniUser> SearchUserAsync(string name, bool descriptionAsHtml = true)
        {
            string query = AniQuery.UserSearchQuery;
            var variables = new { name, asHtml = descriptionAsHtml };

            return await ExecuteQueryAsync(query, variables, _apiClient.GetUserAsync);
        }

        public async Task<AniUser> SearchUserAsync(int id, bool descriptionAsHtml = true)
        {
            string query = AniQuery.UserSearchQuery;
            var variables = new { id, asHtml = descriptionAsHtml };

            return await ExecuteQueryAsync(query, variables, _apiClient.GetUserAsync);
        }

        public async Task<AniStudio> SearchStudioAsync(string name)
        {
            string query = AniQuery.StudioSearchQuery;
            var variables = new { search = name };

            return await ExecuteQueryAsync(query, variables, _apiClient.GetStudioAsync);
        }

        public async Task<AniStudio> SearchStudioAsync(int id)
        {
            string query = AniQuery.StudioSearchQuery;
            var variables = new { id };

            return await ExecuteQueryAsync(query, variables, _apiClient.GetStudioAsync);
        }

        public async Task<AniStaff> SearchStaffAsync(string name, bool descriptionAsHtml = true)
        {
            string query = AniQuery.StaffSearchQuery;
            var variables = new { search = name, asHtml = descriptionAsHtml };

            return await ExecuteQueryAsync(query, variables, _apiClient.GetStaffAsync);
        }

        public async Task<AniStaff> SearchStaffAsync(int id, bool descriptionAsHtml = true)
        {
            string query = AniQuery.StaffSearchQuery;
            var variables = new { id, asHtml = descriptionAsHtml };

            return await ExecuteQueryAsync(query, variables, _apiClient.GetStaffAsync);
        }

        public async Task<AniMedia> SearchMedia(int id, MediaType mediaType, bool descriptionAsHtml = true)
        {
            string query = mediaType == MediaType.ANIME ? AniQuery.AnimeIDQuery : AniQuery.MangaIDQuery;
            var variables = new
            {
                id,
                type = Enum.GetName(typeof(MediaType), mediaType),
                asHtml = descriptionAsHtml
            };

            return await ExecuteQueryAsync(query, variables, _apiClient.GetMediaAsync);
        }

        public async Task<AniMedia> SearchMedia(string name, MediaType mediaType, bool descriptionAsHtml = true)
        {
            string query = mediaType == MediaType.ANIME ? AniQuery.AnimeNameQuery : AniQuery.MangaNameQuery;
            var variables = new
            {
                search = name,
                type = Enum.GetName(typeof(MediaType), mediaType),
                asHtml = descriptionAsHtml
            };

            return await ExecuteQueryAsync(query, variables, _apiClient.GetMediaAsync);
        }

        public async Task<AniCharacter> SearchCharacterAsync(string name, bool descriptionAsHtml = true)
        {
            string query = AniQuery.CharacterSearchQuery;
            var variables = new { search = name, asHtml = descriptionAsHtml };

            return await ExecuteQueryAsync(query, variables, _apiClient.GetCharacterAsync);
        }

        public async Task<AniCharacter> SearchCharacterAsync(int id, bool descriptionAsHtml = true)
        {
            string query = AniQuery.CharacterSearchQuery;
            var variables = new { id, asHtml = descriptionAsHtml };

            return await ExecuteQueryAsync(query, variables, _apiClient.GetCharacterAsync);
        }

        public async Task<Page> GetPagedMediaAsync(int page, int perPage, MediaType mediaType, string? search = null, string sort = "SEARCH_MATCH", bool descriptionAsHtml = true)
        {
            string query = AniQuery.PagedMediaQuery;
            var variables = new
            {
                page,
                perPage,
                type = Enum.GetName(typeof(MediaType), mediaType),
                search,
                sort = new[] { sort },
                asHtml = descriptionAsHtml
            };

            return await ExecuteQueryAsync(query, variables, _apiClient.GetPagedDataAsync<Page>);
        }

        public async Task<Page> GetPagedMediaAsync(int page, int perPage, MediaType mediaType)
        {
            return await GetPagedMediaAsync(page, perPage, mediaType, null, "SEARCH_MATCH", true);
        }

        public async Task<Page> GetPagedCharactersAsync(int page, int perPage, string? search = null, string sort = "SEARCH_MATCH", bool descriptionAsHtml = true)
        {
            string query = AniQuery.PagedCharacterQuery;
            var variables = new
            {
                page,
                perPage,
                search,
                sort = new[] { sort },
                asHtml = descriptionAsHtml
            };

            return await ExecuteQueryAsync(query, variables, _apiClient.GetPagedDataAsync<Page>);
        }

        public async Task<Page> GetPagedStaffAsync(int page, int perPage, string? search = null, string sort = "SEARCH_MATCH", bool descriptionAsHtml = true)
        {
            string query = AniQuery.PagedStaffQuery;
            var variables = new
            {
                page,
                perPage,
                search,
                sort = new[] { sort },
                asHtml = descriptionAsHtml
            };

            return await ExecuteQueryAsync(query, variables, _apiClient.GetPagedDataAsync<Page>);
        }

        public async Task<Page> GetPagedStudiosAsync(int page, int perPage, string? search = null, string sort = "SEARCH_MATCH")
        {
            string query = AniQuery.PagedStudioQuery;
            var variables = new
            {
                page,
                perPage,
                search,
                sort = new[] { sort }
            };

            return await ExecuteQueryAsync(query, variables, _apiClient.GetPagedDataAsync<Page>);
        }

        public async Task<AniMedia> GetMediaTrendAsync(int mediaId)
        {
            string query = AniQuery.MediaTrendQuery;
            var variables = new
            {
                mediaId
            };

            try
            {
                return await ExecuteQueryAsync(query, variables, _apiClient.GetMediaAsync);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching media trend for media ID {mediaId}: {ex.Message}");
                return null;
            }
        }

        public async Task<Page> GetAllMediaTrendsAsync(MediaType mediaType = MediaType.ANIME, int page = 1, int perPage = 10)
        {
            string query = AniQuery.AllMediaTrendsQuery;
            var variables = new
            {
                page,
                perPage,
                type = Enum.GetName(typeof(MediaType), mediaType)
            };

            return await ExecuteQueryAsync(query, variables, _apiClient.GetPagedDataAsync<Page>);
        }

        public async Task<MediaListCollection> GetUserMediaListAsync(int? userId = null, string? userName = null, 
            MediaType mediaType = MediaType.ANIME, MediaListStatus? status = null)
        {
            string query = AniQuery.UserMediaListQuery;
            var variables = new
            {
                userId,
                userName,
                type = Enum.GetName(typeof(MediaType), mediaType),
                status = status.HasValue ? Enum.GetName(typeof(MediaListStatus), status.Value) : null
            };

            return await ExecuteQueryAsync(query, variables, _apiClient.GetMediaListCollectionAsync);
        }

        public async Task<MediaListCollection> GetUserMediaListAsync(int? userId = null, string? userName = null, 
            MediaType mediaType = MediaType.ANIME, string? status = null)
        {
            MediaListStatus? statusEnum = null;
            if (!string.IsNullOrEmpty(status) && Enum.TryParse<MediaListStatus>(status, out var parsedStatus))
            {
                statusEnum = parsedStatus;
            }

            return await GetUserMediaListAsync(userId, userName, mediaType, statusEnum);
        }

        private async Task<T> ExecuteQueryAsync<T>(string query, object variables, Func<string, object, Task<T>> apiMethod)
        {
            try
            {
                return await apiMethod(query, variables);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error occurred: {e}");
                if (typeof(T) == typeof(MediaListCollection))
                {
                    return (T)(object)new MediaListCollection { lists = new List<MediaListGroup>() };
                }
                return default;
            }
        }
    }
}
