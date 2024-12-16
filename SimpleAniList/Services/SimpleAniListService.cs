using SimpleAnilist.AnilistAPI.Enum;
using SimpleAnilist.Models.Character;
using SimpleAnilist.Models.Media;
using SimpleAnilist.Models.Staff;
using SimpleAnilist.Models.Studio;
using SimpleAnilist.Models.User;
using SimpleAnilist.Services.Query;

namespace SimpleAnilist.Services
{
    public class SimpleAniListService
    {
        private readonly GraphQLAnilist _apiClient;

        public SimpleAniListService()
        {
            _apiClient = new GraphQLAnilist();
        }

        public async Task<AniUser> SearchUserAsync(string name)
        {
            string query = AniQuery.UserSearchQuery;
            var variables = new { name, asHtml = true };

            return await ExecuteQueryAsync(query, variables, _apiClient.GetUserAsync);
        }

        public async Task<AniStudio> SearchStudioAsync(string name)
        {
            string query = AniQuery.StudioSearchQuery;
            var variables = new { search = name };

            return await ExecuteQueryAsync(query, variables, _apiClient.GetStudioAsync);
        }

        public async Task<AniStaff> SearchStaffAsync(string name)
        {
            string query = AniQuery.StaffSearchQuery;
            var variables = new { search = name, asHtml = true };

            return await ExecuteQueryAsync(query, variables, _apiClient.GetStaffAsync);
        }

        public async Task<AniMedia> SearchMediaByIdAsync(int id, MediaType mediaType)
        {
            string query = mediaType == MediaType.ANIME ? AniQuery.AnimeIDQuery : AniQuery.MangaIDQuery;
            var variables = new
            {
                id,
                type = Enum.GetName(typeof(MediaType), mediaType),
                asHtml = true
            };

            return await ExecuteQueryAsync(query, variables, _apiClient.GetMediaAsync);
        }

        public async Task<AniMedia> SearchMediaByNameAsync(string name, MediaType mediaType)
        {
            string query = mediaType == MediaType.ANIME ? AniQuery.AnimeNameQuery : AniQuery.MangaNameQuery;
            var variables = new
            {
                search = name,
                type = Enum.GetName(typeof(MediaType), mediaType),
                asHtml = true
            };

            return await ExecuteQueryAsync(query, variables, _apiClient.GetMediaAsync);
        }

        public async Task<AniCharacter> SearchCharacterAsync(string name)
        {
            string query = AniQuery.CharacterSearchQuery;
            var variables = new { search = name, asHtml = true };

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
