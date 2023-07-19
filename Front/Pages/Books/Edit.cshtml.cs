using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Front.Pages.Books;

public class EditModel : PageModel
{
    private readonly HttpClient _httpClient;

    [BindProperty]
    public Book? book { get; set; }

    [BindProperty]
    public IFormFile Photo { get; set; }

    public EditModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task OnGet(string name)
    {
        var response = await _httpClient.GetAsync("http://localhost:5178/api/Books/" + name);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        book = JsonConvert.DeserializeObject<Book>(content);
    }


    public async Task<IActionResult> OnPost(string oldName)
    {
        using(var fs = new FileStream($"wwwroot/photo/{book.PhotoPath}", FileMode.Create))
        {
            Photo.CopyTo(fs);
        }
        book.PhotoPath = Photo.FileName;
        var response = await _httpClient.PutAsJsonAsync("http://localhost:5178/api/Books/" + oldName, book);
        response.EnsureSuccessStatusCode();
        return RedirectToPage("Index");
    }
}
