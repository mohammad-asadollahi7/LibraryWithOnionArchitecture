using Application.Dto;
using Domain;

namespace Application;

public interface IBookService
{
    IEnumerable<BookDto> Get();
    BookDto GetByName(string name);
    BookDto Create(BookDto bookDto);
    void AddCount(string name);
    void Update(string oldName, BookDto bookDto);
    void Delete(string name);

}
