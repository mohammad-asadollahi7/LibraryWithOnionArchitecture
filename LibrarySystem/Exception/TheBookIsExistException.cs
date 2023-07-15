
namespace Domain.Exception;
public class TheBookIsExistsException : DomainException
{
    public override string Message => "The Book is exists. please use the Add button";
}
