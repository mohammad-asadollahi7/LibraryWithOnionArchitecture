
namespace Domain.Exception;

public class ThePriceIsNotValidException : DomainException
{
    public override string Message => "The input price is not valid.";
}
