using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;


namespace Front.Pages.Books;

public class IndexModel : PageModel
{
    public List<Book>? books { get; set; }
    private readonly HttpClient _httpClient;
    private readonly string BaseUrl = "http://localhost:5178/api/Books/";
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


    public async Task OnGetAdd(string name)
    {
        var response = await _httpClient.GetAsync(BaseUrl + "AddCount/" + name);
        response.EnsureSuccessStatusCode();
        await OnGet();
    }
}
