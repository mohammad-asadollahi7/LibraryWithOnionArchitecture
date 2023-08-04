using Application.Dto;
using Application.Exception;
using AutoMapper;
using Domain;
using System.Xml.Linq;

namespace Application;

public class BookService : IBookService
{
    private IBookRepository _bookRepository;
    private readonly IBookRepositoryContainer _bookRepositoryContainer;
    private readonly IMapper _mapper;

    public BookService(IBookRepositoryContainer bookRepositoryContainer, IMapper mapper)
    {
        _bookRepositoryContainer  = bookRepositoryContainer;
        _mapper = mapper;
    }


    public IEnumerable<BookDto> Get()
    {
        _bookRepository = _bookRepositoryContainer.GetBookRepsitoryImplementation("2");
        var books = _bookRepository.GetAll();
        if (books == null)
            throw new ThereIsNoBookException();

        return _mapper.Map<IEnumerable<BookDto>>(books);
    }

    
    public BookDto GetByName(string name)
    {
        _bookRepository = _bookRepositoryContainer.GetBookRepsitoryImplementation("2");

        var IsExist = IsBookExistByName(name);
        if (!IsExist)
            throw new TheBookWasNotFoundException();
        
        var book = _bookRepository.GetByName(name);
        return _mapper.Map<BookDto>(book);
    }


    public BookDto Create(BookDto bookDto)
    {
        _bookRepository = _bookRepositoryContainer.GetBookRepsitoryImplementation("2");

        var book = new Book(_bookRepository, 
                           bookDto.Name, 
                           bookDto.Author, 
                           bookDto.Description,
                           bookDto.PhotoPath, bookDto.Price);

        var submittedBook =  _bookRepository.Create(book);
        return _mapper.Map<BookDto>(submittedBook);
    }

    public void AddCount(string name)
    {
        _bookRepository = _bookRepositoryContainer.GetBookRepsitoryImplementation("2");

        var isExist = IsBookExistByName(name);
        if (!isExist)
            throw new TheBookWasNotFoundException();

        var book = _bookRepository.GetByName(name);
        book.Count++;
        _bookRepository.Update(book);
    }


    public void Update(string oldName,BookDto bookDto)
    {
        _bookRepository = _bookRepositoryContainer.GetBookRepsitoryImplementation("2");

        var isExist = IsBookExistByName(oldName);
        if (!isExist)
            throw new TheBookWasNotFoundException();

        var book = _bookRepository.GetByName(oldName);
        book.Name = bookDto.Name;
        book.Author = bookDto.Author;
        book.Description = bookDto.Description;
        book.PhotoPath = bookDto.PhotoPath;
        book.SetPrice(bookDto.Price);
        
        _bookRepository.Update(book);
    }

    public void Delete(string name)
    {
        _bookRepository = _bookRepositoryContainer.GetBookRepsitoryImplementation("2");

        var isExist = IsBookExistByName(name);
        if (!isExist)
            throw new TheBookWasNotFoundException();
        var book = _bookRepository.GetByName(name);   
        _bookRepository.Delete(book);
    }


    private bool IsBookExistByName(string name)
    {
        _bookRepository = _bookRepositoryContainer.GetBookRepsitoryImplementation("2");

        return _bookRepository.IsExist(name);
    }

}
