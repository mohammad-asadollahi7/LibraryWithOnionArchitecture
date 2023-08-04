
namespace Domain;

public interface IBookRepositoryContainer
{
    public IBookRepository GetBookRepsitoryImplementation(string option);
}
