using Domain;
using Persistence;

namespace Infrastructure;

public class BookRepository : IBookRepository
{
    private BookData _bookData;

    public BookRepository()
    {
        _bookData = new BookData();
    }
    public IEnumerable<Book> GetAll()
    {
        return _bookData.books;
    }

    public Book? GetByName(string name)
    {
        return _bookData.books.FirstOrDefault(b => b.Name == name);
    }


    public bool IsExist(Predicate<Book> predicate)
    {
        return _bookData.books.Exists(predicate);
    }

    public Book Create(Book book)
    {
        _bookData.books.Add(book);
       return book;
    }

    public void Update(Book updatedBook)
    {
        var index = _bookData.books.FindIndex(b => b.Id == updatedBook.Id);
        _bookData.books[index] = updatedBook;
    }

    public void Delete(Book book)
    {
        _bookData.books.Remove(book);
    }

}
