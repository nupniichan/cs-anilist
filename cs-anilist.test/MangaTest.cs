using CsAnilist.Models.Enums;
using CsAnilist.Services;
using NUnit.Framework.Legacy;

namespace cs_anilist.test
{
    [TestFixture]
    public class MangaTests
    {
        private CsAniListService _anilistService;

        [SetUp]
        public void Setup()
        {
            _anilistService = new CsAniListService();
        }

        [Test]
        public async Task SearchMediaById()
        {
            int mangaId = 79835;
            var mediaType = MediaType.MANGA;

            Console.WriteLine($"Testing search for manga ID: {mangaId}");

            var result = await _anilistService.SearchMedia(mangaId, mediaType);

            // Debug info
            Console.WriteLine($"Found manga: '{result.title.english ?? result.title.romaji}' (ID: {result.id})");
            Console.WriteLine($"Format: {result.format}, Status: {result.status}");

            ClassicAssert.NotNull(result);
            ClassicAssert.AreEqual(mangaId, result.id);
            ClassicAssert.AreEqual(MediaType.MANGA, result.type);
            ClassicAssert.NotNull(result.title);
        }

        [Test]
        public async Task SearchMediaByName()
        {
            string mangaName = "Is the Order a Rabbit";
            var mediaType = MediaType.MANGA;

            Console.WriteLine($"Testing search for manga name: '{mangaName}'");

            var result = await _anilistService.SearchMedia(mangaName, mediaType);

            // Debug info
            Console.WriteLine($"Found manga: '{result.title.english ?? result.title.romaji}' (ID: {result.id})");
            Console.WriteLine($"Japanese title: {result.title.native}");
            Console.WriteLine($"Format: {result.format}, Status: {result.status}");
            Console.WriteLine($"Chapters: {result.chapters}, Volumes: {result.volumes}");

            ClassicAssert.NotNull(result);
            ClassicAssert.NotNull(result.title);
            ClassicAssert.AreEqual(MediaType.MANGA, result.type);
            
            bool titleMatchesEnglish = result.title.english != null && 
                result.title.english.IndexOf("Is the Order a Rabbit", StringComparison.OrdinalIgnoreCase) >= 0;
            bool titleMatchesRomaji = result.title.romaji != null && 
                result.title.romaji.IndexOf("Gochuumon wa Usagi", StringComparison.OrdinalIgnoreCase) >= 0;
            bool titleMatchesNative = result.title.native != null && 
                result.title.native.Contains("ご注文はうさぎ");
            
            ClassicAssert.True(titleMatchesEnglish || titleMatchesRomaji || titleMatchesNative);
        }

        [Test]
        public async Task GetPagedMedia_WithMangaSearch()
        {
            int page = 1;
            int perPage = 5;
            var mediaType = MediaType.MANGA;
            string search = "Is the Order a Rabbit";

            Console.WriteLine($"Testing paged media search for manga with term: '{search}'");

            var result = await _anilistService.GetPagedMediaAsync(page, perPage, mediaType, search);

            // Debug info
            Console.WriteLine($"Found {result.media.Count} results for search '{search}'");
            foreach (var media in result.media)
            {
                Console.WriteLine($"Title: {media.title.english ?? "null"} / {media.title.romaji ?? "null"} / {media.title.native ?? "null"}");
                Console.WriteLine($"  ID: {media.id}, Format: {media.format}, Status: {media.status}");
            }

            ClassicAssert.NotNull(result);
            ClassicAssert.NotNull(result.media);
            ClassicAssert.True(result.media.Count > 0);
            
            // Check if at least one result contains the search term in title (case-insensitive)
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

        [Test]
        public async Task SearchPopularManga_ReturnsSortedResults()
        {
            int page = 1;
            int perPage = 10;
            var mediaType = MediaType.MANGA;
            string sort = MediaSort.POPULARITY_DESC.ToString();

            Console.WriteLine($"Testing popular manga search with sort: {sort}");

            var result = await _anilistService.GetPagedMediaAsync(page, perPage, mediaType, null, sort);

            // Debug info
            Console.WriteLine($"Found {result.media.Count} popular manga");
            for (int i = 0; i < result.media.Count; i++)
            {
                var media = result.media[i];
                Console.WriteLine($"{i+1}. {media.title.english ?? media.title.romaji} - Popularity: {media.popularity}");
            }

            ClassicAssert.NotNull(result);
            ClassicAssert.NotNull(result.media);
            ClassicAssert.True(result.media.Count > 0);
            
            // Verify popular manga are returned (should have high popularity values)
            ClassicAssert.Greater(result.media[0].popularity, 10000); // Highly popular manga should have many fans
            
            // Check popularity is actually sorted
            for (int i = 0; i < result.media.Count - 1; i++)
            {
                ClassicAssert.GreaterOrEqual(result.media[i].popularity, result.media[i + 1].popularity);
            }
        }
        
        [Test]
        public async Task MangaCoverImage_ContainsValidUrls()
        {
            string mangaName = "Is the Order a Rabbit";
            var mediaType = MediaType.MANGA;

            Console.WriteLine($"Testing manga cover image for '{mangaName}'");

            var result = await _anilistService.SearchMedia(mangaName, mediaType);

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
        public async Task MangaFormat_HasValidEnumValue()
        {
            string mangaName = "Is the Order a Rabbit";
            var mediaType = MediaType.MANGA;

            Console.WriteLine($"Testing manga format for '{mangaName}'");

            var result = await _anilistService.SearchMedia(mangaName, mediaType);

            // Debug info
            Console.WriteLine($"Media format: {result.format}");

            ClassicAssert.NotNull(result);
            
            // Format should be a valid enum value
            ClassicAssert.NotNull(result.format);
            
            // Is the Order a Rabbit manga should usually be MANGA format
            ClassicAssert.AreEqual(MediaFormat.MANGA, result.format);
        }
        
        [Test]
        public async Task MangaChaptersAndVolumes_AreRetrieved()
        {
            string mangaName = "Is the Order a Rabbit";
            var mediaType = MediaType.MANGA;

            Console.WriteLine($"Testing manga chapters and volumes for '{mangaName}'");

            var result = await _anilistService.SearchMedia(mangaName, mediaType);

            // Debug info
            Console.WriteLine($"Found manga: {result.title.english ?? result.title.romaji}");
            Console.WriteLine($"Chapters: {result.chapters ?? 0}");
            Console.WriteLine($"Volumes: {result.volumes ?? 0}");

            ClassicAssert.NotNull(result);
            
            // Manga should have chapter/volume info (might be null if ongoing)
            Console.WriteLine($"Manga has chapter data: {result.chapters != null}");
            Console.WriteLine($"Manga has volume data: {result.volumes != null}");
            
            // If chapters/volumes are present, they should be valid
            if (result.chapters.HasValue)
            {
                ClassicAssert.GreaterOrEqual(result.chapters.Value, 0);
            }
            
            if (result.volumes.HasValue)
            {
                ClassicAssert.GreaterOrEqual(result.volumes.Value, 0);
            }
        }

        [Test]
        public async Task GetAllMediaTrends()
        {
            int page = 1;
            int perPage = 10;
            var mediaType = MediaType.MANGA;

            Console.WriteLine($"Testing GetAllMediaTrendsAsync for manga (page: {page}, perPage: {perPage})");

            var result = await _anilistService.GetAllMediaTrendsAsync(mediaType, page, perPage);

            // Debug info
            Console.WriteLine($"Retrieved {result.media?.Count ?? 0} trending manga");
            
            if (result.media != null && result.media.Count > 0)
            {
                Console.WriteLine("Top trending manga:");
                for (int i = 0; i < Math.Min(5, result.media.Count); i++)
                {
                    var media = result.media[i];
                    Console.WriteLine($"{i+1}. {media.title?.english ?? media.title?.romaji ?? "Unknown"} - Trend: {media.trending}");
                    if (media.chapters.HasValue)
                    {
                        Console.WriteLine($"   Chapters: {media.chapters}, Volumes: {media.volumes}");
                    }
                }
            }

            ClassicAssert.NotNull(result);
            ClassicAssert.NotNull(result.media);
            ClassicAssert.True(result.media.Count > 0);
            ClassicAssert.NotNull(result.pageInfo);
            
            // First item should have a trending value
            ClassicAssert.NotNull(result.media[0].trending);
            ClassicAssert.GreaterOrEqual(result.media[0].trending, 0);
            
            // Verify all returned items are manga
            foreach (var media in result.media)
            {
                ClassicAssert.AreEqual(MediaType.MANGA, media.type);
            }
        }
        
        [Test]
        public async Task GetAllMediaTrends_PaginationWorks()
        {
            int page1 = 1;
            int page2 = 2;
            int perPage = 5; 
            var mediaType = MediaType.MANGA;

            Console.WriteLine($"Testing GetAllMediaTrendsAsync pagination for manga");

            var resultPage1 = await _anilistService.GetAllMediaTrendsAsync(mediaType, page1, perPage);
            
            // Debug info for first page
            Console.WriteLine($"Page 1 - Retrieved {resultPage1.media?.Count ?? 0} trending manga");
            Console.WriteLine($"Page info: Current={resultPage1.pageInfo?.currentPage}, HasNext={resultPage1.pageInfo?.hasNextPage}");
            
            ClassicAssert.NotNull(resultPage1);
            ClassicAssert.NotNull(resultPage1.media);
            ClassicAssert.AreEqual(perPage, resultPage1.media.Count);
            ClassicAssert.NotNull(resultPage1.pageInfo);
            ClassicAssert.AreEqual(page1, resultPage1.pageInfo.currentPage);
            
            // Only proceed to test second page if there is one
            if (resultPage1.pageInfo.hasNextPage)
            {
                var resultPage2 = await _anilistService.GetAllMediaTrendsAsync(mediaType, page2, perPage);
                
                // Debug info for second page
                Console.WriteLine($"Page 2 - Retrieved {resultPage2.media?.Count ?? 0} trending manga");
                
                ClassicAssert.NotNull(resultPage2);
                ClassicAssert.NotNull(resultPage2.media);
                ClassicAssert.AreEqual(perPage, resultPage2.media.Count);
                ClassicAssert.NotNull(resultPage2.pageInfo);
                ClassicAssert.AreEqual(page2, resultPage2.pageInfo.currentPage);
                
                // Make sure page 1 and page 2 have different items
                bool hasDifferentItems = !resultPage1.media.Select(m => m.id)
                    .Intersect(resultPage2.media.Select(m => m.id))
                    .Any();
                    
                ClassicAssert.True(hasDifferentItems, "Page 1 and Page 2 should have different manga items");
            }
            else
            {
                Console.WriteLine("No second page available for testing");
            }
        }
        
        [Test]
        public async Task GetAllMediaTrendsManga_CompareWithPopular()
        {
            int page = 1;
            int perPage = 10;
            var mediaType = MediaType.MANGA;
            string sort = MediaSort.POPULARITY_DESC.ToString();

            Console.WriteLine($"Testing trending manga vs popular manga correlation");

            //Get trending manga
            var trendingResult = await _anilistService.GetAllMediaTrendsAsync(mediaType, page, perPage);
            
            //Get popular manga
            var popularResult = await _anilistService.GetPagedMediaAsync(page, perPage, mediaType, null, sort);
            
            // Debug info
            Console.WriteLine($"Retrieved {trendingResult.media?.Count ?? 0} trending manga");
            Console.WriteLine($"Retrieved {popularResult.media?.Count ?? 0} popular manga");
            
            // Log top 3 items from each for comparison
            Console.WriteLine("Top trending manga:");
            for (int i = 0; i < Math.Min(3, trendingResult.media.Count); i++)
            {
                var media = trendingResult.media[i];
                Console.WriteLine($"{i+1}. {media.title?.english ?? media.title?.romaji ?? "Unknown"} - Trend: {media.trending}");
            }
            
            Console.WriteLine("Top popular manga:");
            for (int i = 0; i < Math.Min(3, popularResult.media.Count); i++)
            {
                var media = popularResult.media[i];
                Console.WriteLine($"{i+1}. {media.title?.english ?? media.title?.romaji ?? "Unknown"} - Popularity: {media.popularity}");
            }

            ClassicAssert.NotNull(trendingResult);
            ClassicAssert.NotNull(trendingResult.media);
            ClassicAssert.NotNull(popularResult);
            ClassicAssert.NotNull(popularResult.media);
            
            // Note: We don't assert that these should be identical or even similar
            // This test is mainly for comparison purposes to see the relationship
            // between trending and popularity, which can be different
        }

        // Anilist will return the error code if we spam the request so we have to wait a little
        [OneTimeTearDown]
        public void FreezeTime()
        {
            Thread.Sleep(30000);
        }
    }
} 