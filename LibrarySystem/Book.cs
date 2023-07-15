using Domain.Exception;
using System.Text.Json.Serialization;

namespace Domain;

public class Book
{
    private readonly IBookRepository? _bookRepository;

    [JsonConstructor]
    public Book(string name, string author, int price,
                string description, string photoPath, Guid id, int count)
    {
        Id = id;
        Count = count;
        Name = name;
        Author = author;
        Description = description;
        PhotoPath = photoPath;
        Price = price;

    }

    public Book(IBookRepository? bookRepository,
                string name, string author,
                string Description, string PhotoPath)
    {
        _bookRepository = bookRepository;
        SetValues(name, author, Description, 
                 PhotoPath);
    }

    public Guid Id { get; private set; }
    public int Count { get; set; }
    public string Name { get; set; }
    public string Author { get; set; }
    public int Price { get; private set; }
    public string Description { get; set; }
    public string PhotoPath { get; set; }


    
    public void SetPrice(int price)
    {
        if (price < 0 || price >= 5000000)
        {
            throw new ThePriceIsNotValidException();
        }
        Price = price;  
    }
    
    
    private void SetValues(string name, string author, 
                       string description, string photoPath)
    {
        var IsExist = _bookRepository.IsExist(b => b.Name == name);

        if (IsExist)
        {
            throw new TheBookIsExistsException();
        }
        
        else
        {
            Id = Guid.NewGuid();
            Count = 1;
            Name = name; 
            Author = author;
            Description = description;
            PhotoPath = photoPath;
        }
    }
}