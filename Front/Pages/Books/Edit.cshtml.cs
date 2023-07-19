using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace Front.Pages.Books;

public class EditModel : PageModel
{
    private readonly HttpClient _httpClient;
    private readonly string BaseUrl = "http://localhost:5178/api/Books/";

    public EditModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    [BindProperty]
    public Book? book { get; set; }

    [BindProperty]
    public IFormFile? Photo { get; set; }

    public async Task OnGet(string name)
    {
        var response = await _httpClient.GetAsync(BaseUrl + name);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        book = JsonConvert.DeserializeObject<Book>(content);
    }


    public async Task<IActionResult> OnPost(string oldName)
    {
        ProcessPhoto();
        var response = await _httpClient.PutAsJsonAsync(BaseUrl + oldName, book);
        response.EnsureSuccessStatusCode();
        return RedirectToPage("Index");
    }


    private void ProcessPhoto()
    {
        if (Photo != null)
        {
            using (var fs = new FileStream($"wwwroot/photo/{book.PhotoPath}",
                                                   FileMode.Create))
            {
                Photo.CopyTo(fs);
            }
            book.PhotoPath = Photo.FileName;
        }
    }
}
