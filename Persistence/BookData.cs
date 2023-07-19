using Domain;
using System.Text.Json;

namespace Persistence;

public class BookData
{
    public List<Book>? books;
    private string jsonFilePath;
    public BookData()
    {
        jsonFilePath = GetFilePath();
        var jsonString = File.ReadAllText(jsonFilePath);
        books = JsonSerializer.Deserialize<List<Book>>(jsonString);
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