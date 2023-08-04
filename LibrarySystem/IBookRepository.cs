using Microsoft.Extensions.Primitives;

namespace Domain;

public interface IBookRepository
{
    IEnumerable<Book>? GetAll();
    Book? GetByName(string name);
    bool IsExist(string name);
    Book Create(Book book);
    void Update(Book updatedBook);
    void Delete(Book book);
}
