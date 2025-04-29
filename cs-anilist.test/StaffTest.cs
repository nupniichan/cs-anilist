using CsAnilist.Models.Enums;
using CsAnilist.Models.Staff;
using CsAnilist.Services;
using NUnit.Framework.Legacy;

namespace cs_anilist.test
{
    [TestFixture]
    public class StaffTest
    {
        private CsAniListService _anilistService;

        [SetUp]
        public void Setup()
        {
            _anilistService = new CsAniListService();
        }

        [Test]
        public async Task SearchStaffAsyncByName()
        {
            string staffName = "Key";

            Console.WriteLine($"Testing search for staff name: '{staffName}'");

            var result = await _anilistService.SearchStaffAsync(staffName);

            // Debug info
            Console.WriteLine($"Found staff: {result.name.full()}");
            Console.WriteLine($"Native name: {result.name.native}");
            Console.WriteLine($"Description: {result.description?.Substring(0, Math.Min(result.description?.Length ?? 0, 100))}...");
            Console.WriteLine($"Favorites: {result.favourites}");

            ClassicAssert.NotNull(result);
            ClassicAssert.NotNull(result.name);
            
            // Check if staff name contains the search term
            ClassicAssert.True(result.name.full().IndexOf(staffName, StringComparison.OrdinalIgnoreCase) >= 0,
                "Staff name should contain the search term");
        }

        [Test]
        public async Task SearchStaffAsync_WithDescriptionAsHtml()
        {
            string staffName = "Key";
            bool descriptionAsHtml = true;

            Console.WriteLine($"Testing search for staff with HTML description");

            var result = await _anilistService.SearchStaffAsync(staffName, descriptionAsHtml);

            // Debug info
            Console.WriteLine($"Found staff: {result.name.full()}");
            
            if (!string.IsNullOrEmpty(result.description))
            {
                Console.WriteLine($"Description preview: {result.description.Substring(0, Math.Min(100, result.description.Length))}...");
                Console.WriteLine($"Contains HTML tags: {result.description.Contains("<")}");
            }
            else
            {
                Console.WriteLine("No description available");
            }

            ClassicAssert.NotNull(result);
            
            // If description exists, check if it contains HTML tags when requested
            if (!string.IsNullOrEmpty(result.description) && descriptionAsHtml)
            {
                ClassicAssert.True(result.description.Contains("<"), 
                    "Description should contain HTML tags when descriptionAsHtml is true");
            }
        }

        [Test]
        public async Task GetPagedStaffAsync()
        {
            int page = 1;
            int perPage = 5;

            Console.WriteLine($"Testing paged staff (page: {page}, perPage: {perPage})");

            var result = await _anilistService.GetPagedStaffAsync(page, perPage);

            // Debug info
            Console.WriteLine($"Retrieved {result.staff?.Count ?? 0} staff members");
            Console.WriteLine($"Page info: Current={result.pageInfo.currentPage}, Total={result.pageInfo.total}, HasNext={result.pageInfo.hasNextPage}");
            
            foreach (var staff in result.staff)
            {
                Console.WriteLine($"- {staff.name.full()}");
                Console.WriteLine($"  Favorites: {staff.favourites}");
            }

            ClassicAssert.NotNull(result);
            ClassicAssert.NotNull(result.staff);
            ClassicAssert.AreEqual(perPage, result.staff.Count);
            ClassicAssert.NotNull(result.pageInfo);
            ClassicAssert.AreEqual(page, result.pageInfo.currentPage);
        }

        [Test]
        public async Task GetPagedStaffAsyncWithSearch()
        {
            int page = 1;
            int perPage = 5;
            string search = "Koi";

            Console.WriteLine($"Testing paged staff search with term: '{search}'");

            var result = await _anilistService.GetPagedStaffAsync(page, perPage, search);

            // Debug info
            Console.WriteLine($"Found {result.staff?.Count ?? 0} staff members for search '{search}'");
            
            foreach (var staff in result.staff)
            {
                Console.WriteLine($"- {staff.name.full()}");
            }

            ClassicAssert.NotNull(result);
            ClassicAssert.NotNull(result.staff);
            ClassicAssert.True(result.staff.Count > 0);
            
            // Check if at least one result contains the search term
            bool hasMatchingName = false;
            foreach (var staff in result.staff)
            {
                if (staff.name.full().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    hasMatchingName = true;
                    break;
                }
            }
            
            ClassicAssert.True(hasMatchingName, 
                "At least one staff should match the search term");
        }

        [Test]
        public async Task GetPagedStaffAsync_WithSort()
        {
            int page = 1;
            int perPage = 10;
            string sort = MediaSort.FAVOURITES_DESC.ToString(); 

            Console.WriteLine($"Testing paged staff with sorting: {sort}");

            var result = await _anilistService.GetPagedStaffAsync(page, perPage, null, sort);

            // Debug info
            Console.WriteLine($"Retrieved {result.staff?.Count ?? 0} staff members sorted by favorites");
            
            foreach (var staff in result.staff.Take(5))
            {
                Console.WriteLine($"- {staff.name.full()}");
                Console.WriteLine($"  Favorites: {staff.favourites}");
            }

            ClassicAssert.NotNull(result);
            ClassicAssert.NotNull(result.staff);
            
            // Verify popular staff are returned (should have high favorites count)
            ClassicAssert.Greater(result.staff[0].favourites, 10000); // Popular staff should have many favorites
            
            // Check favorites is actually sorted in descending order
            for (int i = 0; i < result.staff.Count - 1; i++)
            {
                ClassicAssert.GreaterOrEqual(result.staff[i].favourites, result.staff[i + 1].favourites);
            }
        }

        [Test]
        public async Task StaffImage_ContainsValidUrls()
        {
            string staffName = "Key";

            Console.WriteLine($"Testing staff image URLs");

            var result = await _anilistService.SearchStaffAsync(staffName);

            // Debug info
            Console.WriteLine($"Image URLs for {result.name.full()}:");
            Console.WriteLine($"- Large: {result.image.large ?? "null"}");
            Console.WriteLine($"- Medium: {result.image.medium ?? "null"}");

            ClassicAssert.NotNull(result);
            ClassicAssert.NotNull(result.image);
            
            // Image should have valid URLs
            ClassicAssert.False(string.IsNullOrEmpty(result.image.large));
            ClassicAssert.False(string.IsNullOrEmpty(result.image.medium));
            
            // URLs should be actual URLs
            ClassicAssert.True(result.image.large.StartsWith("http"));
            ClassicAssert.True(result.image.medium.StartsWith("http"));
        }

        // Anilist will return the error code if we spam the request so we have to wait a little
        [OneTimeTearDown]
        public void FreezeTime()
        {
            Thread.Sleep(30000);
        }
    }

    // Same as character name
    public static class StaffNameExtensions
    {
        public static string full(this StaffName name)
        {
            if (string.IsNullOrEmpty(name.last))
                return name.first;
            return $"{name.first} {name.last}";
        }
    }
} 