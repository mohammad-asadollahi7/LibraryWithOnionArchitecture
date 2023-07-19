using Domain;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Persistence;

public class BookData
{
    public List<Book> books;

    public BookData()
    {
        var FilePath = GetFilePath();
        var jsonString = File.ReadAllText(FilePath);
        books = JsonSerializer.Deserialize<List<Book>>(jsonString);
    }
      

    private string GetFilePath()
    {
        string? projectPath = new FileInfo(AppDomain.CurrentDomain.BaseDirectory)?
                                .Directory?.Parent?.Parent?.Parent?.Parent?.FullName;

        string? jsonFilePath = Path.Combine(projectPath, "Persistence", "Books.json");
        string? photoFilePath = Path.Combine(projectPath, "Persistence", "Photos");
        return jsonFilePath;
    }

}