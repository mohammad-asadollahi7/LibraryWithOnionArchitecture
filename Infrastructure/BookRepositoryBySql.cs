using Domain;
using Newtonsoft.Json;
using Persistence;


namespace Infrastructure;

public class BookRepositoryBySql : IBookRepository
{
    private readonly BookDataFromDatabase _bookData;

    public BookRepositoryBySql(BookDataFromDatabase bookData)
    {
        _bookData = bookData;
    }
    public Book Create(Book book)
    {
        _bookData.ExecutedCommand($"INSERT INTO dbo.Book " +
                         $"VALUES('{book.Id}', '{book.Count}', '{book.Name}'," +
                         $"'{book.Author}', '{book.Price}', " +
                         $"'{book.Description}', '{book.PhotoPath}')");
        return book;
    }


    public void Delete(Book book)
    {
        _bookData.ExecutedCommand($"DELETE FROM dbo.Book WHERE Id = '{book.Id}'");
    }

    public IEnumerable<Book>? GetAll()
    {
        var stringJson = _bookData.ExecutedQuery("SELECT * FROM dbo.Book");
        return JsonConvert.DeserializeObject<IEnumerable<BookDataModel>>(stringJson);
    }

    public Book? GetByName(string name)
    {
        var stringJson = _bookData.ExecutedQuery($"SELECT * FROM dbo.Book WHERE Name = '{name}'");
        var jsonList = JsonConvert.DeserializeObject<List<BookDataModel>>(stringJson);
        return jsonList[0];
    }

    public bool IsExist(string name)
    {
        var stringJson = _bookData.ExecutedQuery($"SELECT * FROM dbo.Book WHERE Name = '{name}'");
        return stringJson != "[]";

    }

    public void Update(Book updatedBook)
    {
        _bookData.ExecutedCommand($"UPDATE dbo.Book SET Count = '{updatedBook.Count}'," +
                                  $"Name = '{updatedBook.Name}', Author = '{updatedBook.Author}', " +
                                  $"Price = '{updatedBook.Price}', Description = '{updatedBook.Description}'," +
                                  $"PhotoPath = '{updatedBook.PhotoPath}' WHERE Id = '{updatedBook.Id}'");
    }
}
