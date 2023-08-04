using Domain;
using System.Text.Json;

namespace Persistence;

public class BookDataFromJson
{
    public List<Book>? books;
    private string jsonFilePath;
    public BookDataFromJson()
    {
        jsonFilePath = GetFilePath();
        var jsonString = File.ReadAllText(jsonFilePath);
        List<BookDataModel>? bookDataModels = JsonSerializer.Deserialize<List<BookDataModel>>(jsonString);
        books = bookDataModels.Cast<Book>().ToList();
    }


    public void SaveChanges()
    {
        var jsonString = JsonSerializer.Serialize(books);
        File.WriteAllText(jsonFilePath, jsonString);
    }

    private string GetFilePath()
    {
        string? projectPath = new FileInfo(AppDomain.CurrentDomain.BaseDirectory)?
                                .Directory?.Parent?.Parent?.Parent?.Parent?.FullName;

        string? jsonFilePath = Path.Combine(projectPath, "Persistence", "Books.json");
        return jsonFilePath;
    }
}