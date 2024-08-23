// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using Bibliotek;

class Program
{
    private static string jSon = "biblo.json";
    private static string jSonUser = "user.json";
    //liste med users
    static List<User> users = new List<User>();
    
    static void Main(string[] args)
    {
        //liste med bøger af book klassen
        List<Book> bøger = FlereBøger();
        users = FlereUser();
        
        
        while (true)
        {
            Console.WriteLine("1. Tilføj bog");
            Console.WriteLine("2. Slet bog");
            Console.WriteLine("3. Opdater bog");
            Console.WriteLine("4. Udlej bog");
            Console.WriteLine("5. Aflever bog");
            Console.WriteLine("6. Tilføj bruger");
         
            string input = Console.ReadLine();
            //switchcase med metoder
            switch (input) 
            {
                case "1":
                    TilføjBog(bøger);
                    break;
                 case "2":
                    SletBog(bøger);
                     break;
                case "3":
                     OpdaterBog(bøger);
                    break;
                case "4":
                    UdlejBog(bøger);
                    break;
                case "5":
                    AfleverBog(bøger);
                    break;
                case "6":
                    OpretBruger(users);
                    break;
                default:
                    Console.WriteLine("Hvad pokker har du skrevet? Prøv igen");
                    break;
             }
        }
    }
    //tilføjer bog
    static void TilføjBog(List<Book> bøger)
    {
        Console.WriteLine("Skriv en titel: ");
        string title = Console.ReadLine();
        Console.WriteLine("Skriv årstal: ");
        int year = int.Parse(Console.ReadLine());
        Console.WriteLine("Skriv forfatter");
        string author = Console.ReadLine();
        Console.WriteLine("Skriv ISBN: ");
        string isbn = Console.ReadLine();
        //tilføjer den titel, år, forfatter og isbn
        bøger.Add(new Book { Title = title, Year = year, Author = author, ISBN = isbn });
        //kalder gemme bøger metode
        GemBøger(bøger);
        Console.WriteLine("Bog nu system");
    }

    static void SletBog(List<Book> bøger)
    {
        Console.WriteLine("Skriv ISBN på bogen der skal slettes");
        string isbn = Console.ReadLine();
        //finder om bog mathcer med skrevet isbn nummer
        Book book = bøger.Find(b => b.ISBN == isbn);
        if (book != null)
        {   
            //fjerner og gemmer bog fra liste
            bøger.Remove(book);
            GemBøger(bøger);
            Console.WriteLine("Bog nu væk");
        }
        else
        {
            Console.WriteLine("Bog ik her?");
        }
    }

    static void OpdaterBog(List<Book> bøger)
    {
        Console.WriteLine("Skriv ISBN på bogen opdater: ");
        string isbn = Console.ReadLine();
        //finder bog med matchene isbn
        Book book = bøger.Find(b => b.ISBN == isbn);
        if (book != null)
        {   
            //opdatere bog med de nye inputs
            Console.WriteLine("Opdater titel på: ");
            book.Title = Console.ReadLine();
            Console.WriteLine("Opdater år:");
            book.Year = int.Parse(Console.ReadLine());
            Console.WriteLine("Opdater formutter: ");
            book.Author = Console.ReadLine();
            
            GemBøger(bøger);
            Console.WriteLine("Bog nu frisk igen");
        }
        else
        {
            Console.WriteLine("Bog ik her?");
        }
    }

    static void UdlejBog(List<Book> bøger)
    {
        Console.WriteLine("Indtast dit ID");
        string userId = Console.ReadLine();
        User user = users.Find(u => u.ID == userId);

        if (user != null)
        {
            Console.WriteLine("Skriv ISBN på din bog");
            string isbn = Console.ReadLine();
            Book book = bøger.Find(b => b.ISBN == isbn);

            if (book != null)
            {
                user.LejBøger(book);
                GemBrugere(users);
            }
            else
            {
                Console.WriteLine("Bog findes ikke");
            }
        }
        else
        {
            Console.WriteLine("Den bruger du har skrevet findes faktisk ikke");
        }
    }

    static void AfleverBog(List<Book> bøger)
    {
        Console.WriteLine("Indtast dit ID");
        string userId = Console.ReadLine();
        User user = users.Find(u => u.ID == userId);

        if (user != null)
        {
            Console.WriteLine("Skriv ISBN på bogen skal skal afleveres");
            string isbn = Console.ReadLine();
            Book book = bøger.Find(b => b.ISBN == isbn);

            if (book != null)
            {
                user.AflevereBog(book);
                GemBrugere(users);
            }
            else
            {
                Console.WriteLine("Bog findes ikke");
            }
        }
        else
        {
            Console.WriteLine("Bruger findes ikke");
        }
    }

    static void OpretBruger(List<User> user)
    {   
        //opretter bruger med skrevet navn og id
        Console.WriteLine("Skriv dit navn");
        string name = Console.ReadLine();
        Console.WriteLine("Indtast dit ID");
        string id = Console.ReadLine();
        //add en ny til user med navn og id
        users.Add(new User { Name = name, ID = id });
        GemBrugere(user);
        Console.WriteLine("Bruger er nu oprettet ");
        
    }
    
    static List<Book> FlereBøger()
    {
        //loader listen af bøgerne ind i json filen (libary.json)
        if (File.Exists(jSon))
        {
            string json = File.ReadAllText(jSon);
            return JsonSerializer.Deserialize<List<Book>>(json);
        }
        return new List<Book>();
    }

    static void GemBøger(List<Book> bøger)
    {
        //gemmer bøgerne i Ib's smarte json serializer 
        string json = JsonSerializer.Serialize(bøger, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(jSon, json);
    }
    
    static List<User> FlereUser()
    {
        //loader listen af bøgerne ind i json filen (user.json)
        if (File.Exists(jSonUser))
        {
            string json = File.ReadAllText(jSonUser);
            return JsonSerializer.Deserialize<List<User>>(json);
        }
        return new List<User>();
    }
    
    static void GemBrugere(List<User> users)
    {
        //gemmer bøgerne i Ib's smarte json serializer 
        string json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(jSonUser, json);
    }

    
    }