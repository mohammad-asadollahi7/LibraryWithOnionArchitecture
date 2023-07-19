using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;


namespace Front.Pages.Books
{
    public class IndexModel : PageModel
    {
        private HttpClient _httpClient;
        public List<Book>? books { get; set; } = new List<Book>();
        private string BaseUrl = @"http://localhost:5178/api/Books/";
        public IndexModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task OnGet()
        {
            var response = await _httpClient.GetAsync(BaseUrl);
            response.EnsureSuccessStatusCode();
            var jsonContent = await response.Content.ReadAsStringAsync();
            books = JsonConvert.DeserializeObject<List<Book>>(jsonContent);
        }

        
        public async Task OnGetDelete(string name)
        {
            var response = await _httpClient.DeleteAsync(BaseUrl + name);
            response.EnsureSuccessStatusCode();
            await OnGet();
        }
    }
}
