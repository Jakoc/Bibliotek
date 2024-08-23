namespace Bibliotek;

public class User
{
    public string Name { get; set; }
    public string ID { get; set; }
    public List<Book> LejetBøger { get; set; } = new List<Book>();
    
    //metode så user kan leje bøger
    public void LejBøger(Book book)
    {
        //hvis lejet ikke bøger ikke er sandt så tilføj bog til lejetbøger
        if (!LejetBøger.Contains(book))
        {
            LejetBøger.Add(book);
            Console.WriteLine($"{Name} HAR NU LEJET: {book.Title}");
        }
        //hvis bog er lejet
        else
        {
            Console.WriteLine($"{book.Title} er enten allerede lejet ud eller død");
        }
    }

    public void AflevereBog(Book book)
    {
        //fjerne bøger fra lejetbøger
        if (LejetBøger.Contains(book))
        {
            LejetBøger.Remove(book);
            Console.WriteLine($"{Name} HAR NU AFLEVERET: {book.Title}");
        }
        else
        {
            Console.WriteLine("Du har aldrig haft denne bog");
        }
    }
}