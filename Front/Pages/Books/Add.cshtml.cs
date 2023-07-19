using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Front.Pages.Books;

public class AddModel : PageModel
{
    private HttpClient _httpClient;

    public AddModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public void OnGet()
    {
        
    }

    [BindProperty]
    public Book book { get; set; }

    [BindProperty]
    public IFormFile Photo { get; set; }
    public async Task<IActionResult> OnPost() 
    {
        book.PhotoPath = Photo.FileName;
        var response = await _httpClient.PostAsJsonAsync("http://localhost:5178/api/Books/", book);
        response.EnsureSuccessStatusCode();
        using(var fs = new FileStream($"wwwroot/photo/{book.PhotoPath}", FileMode.Create))
        {
            Photo.CopyTo(fs);
        }
        return RedirectToPage("Index");
    }

}
