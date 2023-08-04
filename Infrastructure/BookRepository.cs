using Domain;
using Persistence;

namespace Infrastructure;

public class BookRepository : IBookRepository
{
    private BookDataFromJson _bookData;

    public BookRepository(BookDataFromJson bookData)
    {
        _bookData = bookData;
    }
    public IEnumerable<Book>? GetAll()
    {
        return _bookData.books;
    }

    public Book? GetByName(string name)
    {
        return _bookData.books.FirstOrDefault(b => b.Name == name);
    }


    public bool IsExist(string name)
    {
        return _bookData.books.Exists(n => n.Name == name);
    }

    public Book Create(Book book)
    {
       _bookData.books.Add(book);
        _bookData.SaveChanges();
        return book;
    }

    public void Update(Book updatedBook)
    {
        var index = _bookData.books.FindIndex(b => b.Id == updatedBook.Id);
        _bookData.books[index] = updatedBook;
        _bookData.SaveChanges();
    }

    public void Delete(Book book)
    {
        _bookData.books.Remove(book);
        _bookData.SaveChanges();
    }

}
