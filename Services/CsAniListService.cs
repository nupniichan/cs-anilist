using CsAnilist.AnilistAPI.Enum;
using CsAnilist.Models.Character;
using CsAnilist.Models.Media;
using CsAnilist.Models.Staff;
using CsAnilist.Models.Studio;
using CsAnilist.Models.User;
using CsAnilist.Services.Query;

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

        public async Task<AniStudio> SearchStudioAsync(string name)
        {
            string query = AniQuery.StudioSearchQuery;
            var variables = new { search = name };

            return await ExecuteQueryAsync(query, variables, _apiClient.GetStudioAsync);
        }

        public async Task<AniStaff> SearchStaffAsync(string name, bool descriptionAsHtml = true)
        {
            string query = AniQuery.StaffSearchQuery;
            var variables = new { search = name, asHtml = descriptionAsHtml };

            return await ExecuteQueryAsync(query, variables, _apiClient.GetStaffAsync);
        }

        public async Task<AniMedia> SearchMediaByIdAsync(int id, MediaType mediaType, bool descriptionAsHtml = true)
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

        public async Task<AniMedia> SearchMediaByNameAsync(string name, MediaType mediaType, bool descriptionAsHtml = true)
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

        private async Task<T> ExecuteQueryAsync<T>(string query, object variables, Func<string, object, Task<T>> apiMethod)
        {
            try
            {
                return await apiMethod(query, variables);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error occurred: {e}");
                return default;
            }
        }
    }
}
