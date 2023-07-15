using Domain.Exception;

namespace Application.Exception;

public class TheBookWasNotFoundException : DomainException
{
    public override string Message => "The Book with your input name was not found.";
}
