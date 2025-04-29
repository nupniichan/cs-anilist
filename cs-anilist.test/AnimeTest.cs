using CsAnilist.Models.Enums;
using CsAnilist.Services;
using NUnit.Framework.Legacy;

namespace cs_anilist.test
{
    // Those testcase below are for anime only
    // But i only test some information not all of them as same as all the testcases i created
    [TestFixture]
    public class AnimeTest
    {
        private CsAniListService _anilistService;

        [SetUp]
        public void Setup()
        {
            _anilistService = new CsAniListService();
        }

        // Search by mediaId
        [Test]
        public async Task SearchMediaById()
        {
            int animeId = 104198; // this is a valid id
            var mediaType = MediaType.ANIME; // type anime of course

            Console.WriteLine($"Testing search for anime ID: {animeId}");

            var result = await _anilistService.SearchMedia(animeId, mediaType);

            // Debug info
            Console.WriteLine($"Found anime: '{result.title.english ?? result.title.romaji}' (ID: {result.id})");
            Console.WriteLine($"Format: {result.format}, Status: {result.status}");

            ClassicAssert.NotNull(result);
            ClassicAssert.AreEqual(animeId, result.id);
            ClassicAssert.AreEqual(MediaType.ANIME, result.type);
            ClassicAssert.NotNull(result.title);
        }

        // Search media by name
        [Test]
        public async Task SearchMediaByName()
        {
            string animeName = "Is the Order a Rabbit? BLOOM";
            var mediaType = MediaType.ANIME;

            Console.WriteLine($"Testing search for anime name: '{animeName}'");

            var result = await _anilistService.SearchMedia(animeName, mediaType);

            // Debug info
            Console.WriteLine($"Found anime: '{result.title.english ?? result.title.romaji}' (ID: {result.id})");
            Console.WriteLine($"Japanese title: {result.title.native}");
            Console.WriteLine($"Format: {result.format}, Status: {result.status}");

            ClassicAssert.NotNull(result);
            ClassicAssert.NotNull(result.title);
            ClassicAssert.AreEqual(MediaType.ANIME, result.type);

            // Those are the title fields, if it found then the testcase is passed
            bool titleMatchesEnglish = result.title.english != null && result.title.english.Contains("Is the Order a Rabbit");
            bool titleMatchesRomaji = result.title.romaji != null && result.title.romaji.Contains("Gochuumon wa Usagi Desu ka");
            bool titleMatchesNative = result.title.native != null && result.title.native.Contains("ご注文はうさぎですか？BLOOM");
            
            ClassicAssert.True(titleMatchesEnglish || titleMatchesRomaji || titleMatchesNative);
        }

        // Get media by page
        [Test]
        public async Task GetPagedMedia()
        {
            int page = 1;
            int perPage = 5;
            var mediaType = MediaType.ANIME;
            string search = "Is the Order a Rabbit";

            Console.WriteLine($"Testing paged media search with term: '{search}'");

            var result = await _anilistService.GetPagedMediaAsync(page, perPage, mediaType, search);
 
            Console.WriteLine($"Found {result.media.Count} results for search '{search}'");
            foreach (var media in result.media)
            {
                Console.WriteLine($"Title: {media.title.english ?? "null"} / {media.title.romaji ?? "null"} / {media.title.native ?? "null"}");
            }

            ClassicAssert.NotNull(result);
            ClassicAssert.NotNull(result.media);
            ClassicAssert.True(result.media.Count > 0);
            
            // Check if at least one result contains the search term in title
            bool hasMatchingTitle = false;
            foreach (var media in result.media)
            {
                bool matchesEnglish = media.title.english != null &&
                    media.title.english.IndexOf("Is the Order a Rabbit", StringComparison.OrdinalIgnoreCase) >= 0;

                bool matchesRomaji = media.title.romaji != null &&
                    media.title.romaji.IndexOf("Gochuumon", StringComparison.OrdinalIgnoreCase) >= 0;

                bool matchesNative = media.title.native != null &&
                    media.title.native.Contains("ご注文");

                if (matchesEnglish || matchesRomaji || matchesNative)
                {
                    hasMatchingTitle = true;
                    break;
                }
            }
            ClassicAssert.True(hasMatchingTitle);
        }

        // Get trending value by specific mediaId
        [Test]
        public async Task GetMediaTrend()
        {
            int mediaId = 4181; // Valid id, dont ask me why i chose this lol

            Console.WriteLine($"Testing media trend for ID: {mediaId}");

            var result = await _anilistService.GetMediaTrendAsync(mediaId);

            // Debug info
            Console.WriteLine($"Retrieved trend data for: {result.title.english ?? result.title.romaji}");
            Console.WriteLine($"Trend value: {result.trending}");

            ClassicAssert.NotNull(result);
            ClassicAssert.AreEqual(mediaId, result.id);
            ClassicAssert.NotNull(result.title);

            // Trending should be available but might be 0 sometimes
            ClassicAssert.IsTrue(result.trending >= 0);
        }

        // Search media but this function will check more data than the previous one
        [Test]
        public async Task SearchMedia()
        {
            int animeId = 104198;
            var mediaType = MediaType.ANIME;

            Console.WriteLine($"Testing comprehensive media properties for ID: {animeId}");

            var media = await _anilistService.SearchMedia(animeId, mediaType);

            Console.WriteLine($"Retrieved media: {media.title.english ?? media.title.romaji}");
            Console.WriteLine($"Properties:");
            Console.WriteLine($"- ID: {media.id}");
            Console.WriteLine($"- Type: {media.type}");
            Console.WriteLine($"- Format: {media.format}");
            Console.WriteLine($"- Status: {media.status}");
            Console.WriteLine($"- Episodes: {media.episodes}");
            Console.WriteLine($"- Start Date: {media.startDate?.year}-{media.startDate?.month}-{media.startDate?.day}");
            Console.WriteLine($"- Popularity: {media.popularity}");
            Console.WriteLine($"- Average Score: {media.averageScore}");

            ClassicAssert.NotNull(media);
            
            // Basic information
            ClassicAssert.Greater(media.id, 0);
            ClassicAssert.NotNull(media.title);
            ClassicAssert.NotNull(media.type);
            
            // Description
            ClassicAssert.NotNull(media.description);
            
            // Dates
            ClassicAssert.NotNull(media.startDate);
            
            // Statistics
            ClassicAssert.NotNull(media.averageScore);
            ClassicAssert.NotNull(media.popularity);
            
            // Media metadata
            ClassicAssert.NotNull(media.genres);
            
            // Images
            ClassicAssert.NotNull(media.coverImage);
            
            // Related entities
            if (media.studios != null)
            {
                ClassicAssert.NotNull(media.studios.edges);
            }
            
            if (media.characters != null)
            {
                ClassicAssert.NotNull(media.characters.edges);
            }
        }
        
        [Test]
        public async Task SearchPopularAnime()
        {
            int page = 1;
            int perPage = 10;
            var mediaType = MediaType.ANIME;
            string sort = MediaSort.POPULARITY_DESC.ToString();

            Console.WriteLine($"Testing popular anime search with sort: {sort}");

            var result = await _anilistService.GetPagedMediaAsync(page, perPage, mediaType, null, sort);

            // Debug info
            Console.WriteLine($"Found {result.media.Count} popular anime");
            for (int i = 0; i < result.media.Count; i++)
            {
                var media = result.media[i];
                Console.WriteLine($"{i+1}. {media.title.english ?? media.title.romaji} - Popularity: {media.popularity}");
            }

            ClassicAssert.NotNull(result);
            ClassicAssert.NotNull(result.media);
            ClassicAssert.True(result.media.Count > 0);
            
            // Verify popular anime are returned (should have high popularity values)
            ClassicAssert.Greater(result.media[0].popularity, 10000);

            // Check popularity is actually sorted
            for (int i = 0; i < result.media.Count - 1; i++)
            {
                ClassicAssert.GreaterOrEqual(result.media[i].popularity, result.media[i + 1].popularity);
            }
        }
        
        [Test]
        public async Task MediaTitle_ContainsAllTitleFields()
        {
            int animeId = 104198; // Is the Order a Rabbit? BLOOM
            var mediaType = MediaType.ANIME;

            Console.WriteLine($"Testing media title fields for ID: {animeId}");

            var result = await _anilistService.SearchMedia(animeId, mediaType);

            // Debug info
            Console.WriteLine($"Title fields:");
            Console.WriteLine($"- English: {result.title.english ?? "null"}");
            Console.WriteLine($"- Romaji: {result.title.romaji ?? "null"}");
            Console.WriteLine($"- Native: {result.title.native ?? "null"}");

            ClassicAssert.NotNull(result);
            ClassicAssert.NotNull(result.title);
            
            bool hasEnglish = !string.IsNullOrEmpty(result.title.english);
            bool hasRomaji = !string.IsNullOrEmpty(result.title.romaji);
            bool hasNative = !string.IsNullOrEmpty(result.title.native);
            
            ClassicAssert.True(hasEnglish || hasRomaji || hasNative);
        }
        
        [Test]
        public async Task MediaCoverImage_ContainsValidUrls()
        {
            int animeId = 104198; // Is the Order a Rabbit? BLOOM
            var mediaType = MediaType.ANIME;

            Console.WriteLine($"Testing media cover image for ID: {animeId}");

            var result = await _anilistService.SearchMedia(animeId, mediaType);

            // Debug info
            Console.WriteLine($"Cover Image URLs:");
            Console.WriteLine($"- Large: {result.coverImage.large ?? "null"}");
            Console.WriteLine($"- Medium: {result.coverImage.medium ?? "null"}");

            ClassicAssert.NotNull(result);
            ClassicAssert.NotNull(result.coverImage);
            
            // Cover image should have valid URLs
            ClassicAssert.False(string.IsNullOrEmpty(result.coverImage.large));
            ClassicAssert.False(string.IsNullOrEmpty(result.coverImage.medium));
            
            // URLs should be actual URLs
            ClassicAssert.True(result.coverImage.large.StartsWith("http"));
        }
        
        [Test]
        public async Task MediaCharacters_ReturnsValidCharacterData()
        {
            int animeId = 104198; // Is the Order a Rabbit? BLOOM
            var mediaType = MediaType.ANIME;

            Console.WriteLine($"Testing media character data for ID: {animeId}");

            var result = await _anilistService.SearchMedia(animeId, mediaType);

            // Debug info
            if (result.characters != null && result.characters.edges != null && result.characters.edges.Count > 0)
            {
                Console.WriteLine($"Found {result.characters.edges.Count} characters");
                for (int i = 0; i < Math.Min(5, result.characters.edges.Count); i++)
                {
                    var character = result.characters.edges[i].node;
                    Console.WriteLine($"{i+1}. {character.name.first} {character.name.last} ({character.name.native})");
                }
            }
            else
            {
                Console.WriteLine("No characters found");
            }

            ClassicAssert.NotNull(result);
            ClassicAssert.NotNull(result.characters);
            ClassicAssert.NotNull(result.characters.edges);
            ClassicAssert.True(result.characters.edges.Count > 0);
            
            // Check first character has valid data
            var firstCharacter = result.characters.edges[0].node;
            ClassicAssert.Greater(firstCharacter.id, 0);
            ClassicAssert.NotNull(firstCharacter.name);
            
            bool hasFirstName = !string.IsNullOrEmpty(firstCharacter.name.first);
            bool hasLastName = !string.IsNullOrEmpty(firstCharacter.name.last);
            bool hasNativeName = !string.IsNullOrEmpty(firstCharacter.name.native);
            
            // Ensure the character has at least one name component
            ClassicAssert.True(hasFirstName || hasLastName || hasNativeName, 
                "Character should have at least one name component (first, last or native)");
        }
        
        [Test]
        public async Task MediaFormat_HasValidEnumValue()
        {
            int animeId = 104198; // Is the Order a Rabbit? BLOOM
            var mediaType = MediaType.ANIME;

            Console.WriteLine($"Testing media format for ID: {animeId}");

            var result = await _anilistService.SearchMedia(animeId, mediaType);

            // Debug info
            Console.WriteLine($"Media format: {result.format}");

            ClassicAssert.NotNull(result);
            
            // Format should be a valid enum value
            ClassicAssert.NotNull(result.format);
            
            // Is the Order a Rabbit? BLOOM must be a TV format
            ClassicAssert.AreEqual(MediaFormat.TV, result.format);
        }
        
        [Test]
        public async Task MediaStatus_HasValidEnumValue()
        {
            int animeId = 104198; // Is the Order a Rabbit? BLOOM
            var mediaType = MediaType.ANIME;

            Console.WriteLine($"Testing media status for ID: {animeId}");

            var result = await _anilistService.SearchMedia(animeId, mediaType);

            // Debug info
            Console.WriteLine($"Media status: {result.status}");

            ClassicAssert.NotNull(result);
            
            ClassicAssert.NotNull(result.status);
            
            // Is the Order a Rabbit? BLOOM has finished airing
            ClassicAssert.AreEqual(MediaStatus.FINISHED, result.status);
        }

        [Test]
        public async Task GetAllMediaTrendsAnime()
        {
            int page = 1;
            int perPage = 10;
            var mediaType = MediaType.ANIME;

            Console.WriteLine($"Testing GetAllMediaTrendsAsync for anime (page: {page}, perPage: {perPage})");

            var result = await _anilistService.GetAllMediaTrendsAsync(mediaType, page, perPage);

            // Debug info
            Console.WriteLine($"Retrieved {result.media?.Count ?? 0} trending anime");
            
            if (result.media != null && result.media.Count > 0)
            {
                Console.WriteLine("Top trending anime:");
                for (int i = 0; i < Math.Min(5, result.media.Count); i++)
                {
                    var media = result.media[i];
                    Console.WriteLine($"{i+1}. {media.title?.english ?? media.title?.romaji ?? "Unknown"} - Trend: {media.trending}");
                }
            }

            ClassicAssert.NotNull(result);
            ClassicAssert.NotNull(result.media);
            ClassicAssert.True(result.media.Count > 0);
            ClassicAssert.NotNull(result.pageInfo);
            
            // First item should have a trending value
            ClassicAssert.NotNull(result.media[0].trending);
            ClassicAssert.GreaterOrEqual(result.media[0].trending, 0);
            
            // Verify all returned items are anime
            foreach (var media in result.media)
            {
                ClassicAssert.AreEqual(MediaType.ANIME, media.type);
            }
        }
        
        [Test]
        public async Task GetAllMediaTrends_Anime_HandlesLargePage()
        {
            int page = 1;
            int perPage = 25; // Test with larger page size
            var mediaType = MediaType.ANIME;

            Console.WriteLine($"Testing GetAllMediaTrendsAsync with larger page size: {perPage}");

            var result = await _anilistService.GetAllMediaTrendsAsync(mediaType, page, perPage);

            // Debug info
            Console.WriteLine($"Retrieved {result.media?.Count ?? 0} trending anime");
            Console.WriteLine($"Page info: Current={result.pageInfo?.currentPage}, HasNext={result.pageInfo?.hasNextPage}");

            // ClassicAssert
            ClassicAssert.NotNull(result);
            ClassicAssert.NotNull(result.media);
            ClassicAssert.AreEqual(perPage, result.media.Count);
            
            // Check if trending values are returned and are valid
            bool hasTrendingValues = result.media.Any(m => m.trending > 0);
            ClassicAssert.True(hasTrendingValues, "At least one anime should have a positive trending value");
        }

        // Anilist will return the error code if we spam the request so we have to wait a little
        [OneTimeTearDown]
        public void FreezeTime()
        {
            Thread.Sleep(30000);
        }
    }
}
