using Domain.Exception;

namespace Application.Exception;

public class ThereIsNoBookException : DomainException
{
    public override string Message => "There is no book in the database.";
}
