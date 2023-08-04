using Domain;

namespace Persistence;

public class BookDataModel : Book
{
    public BookDataModel(string name, string author, int price,
                string description, string photoPath,
                Guid id, int count) : base(name, author, price,
                 description, photoPath, id, count)
    { }
}

