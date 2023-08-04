
using Domain;
using Persistence;


namespace Infrastructure;

public class BookRepositoryContainer : IBookRepositoryContainer
{
    public BookRepositoryBySql bookRepositoryBySql;
    public BookRepository bookRepository;

    public BookRepositoryContainer()
    {
        bookRepository = new BookRepository(new BookDataFromJson());
        bookRepositoryBySql = new BookRepositoryBySql(new BookDataFromDatabase());
    }
    public IBookRepository GetBookRepsitoryImplementation(string option)
    {
        if (option == "1") 
            return bookRepository;

        else if (option == "2")
            return bookRepositoryBySql;

        else
            throw new Exception();
    }
}
