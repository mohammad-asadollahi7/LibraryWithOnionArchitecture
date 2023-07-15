using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Front.Pages.Books
{
    public class IndexModel : PageModel
    {
        private HttpClient _httpClient;
        public List<Book>? books { get; set; } = new List<Book>();

        public IndexModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task OnGet()
        {
            var response = await _httpClient.GetAsync("http://localhost:5178/api/Books");
            response.EnsureSuccessStatusCode();
            var jsonContent = await response.Content.ReadAsStringAsync();
            books = JsonSerializer.Deserialize<List<Book>>(jsonContent);
        }
    }
}
