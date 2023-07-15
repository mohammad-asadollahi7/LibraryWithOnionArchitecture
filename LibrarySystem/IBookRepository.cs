namespace Domain;

public interface IBookRepository
{
    IEnumerable<Book> GetAll();
    Book? GetByName(string name);
    bool IsExist(Predicate<Book> predicate);
    Book Create(Book book);
    void Update(Book updatedBook);
    void Delete(Book book);
}
