using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public double Copies { get; set; }
    public double Year { get; set; }

    public Book(string title, string author, double copies, double year)
    {
        Title = title;
        Author = author;
        Copies = copies;
        Year = year;
    }
}

class Program
{
    static void GenerateLibraryData(string filePath)
    {
        var books = new List<Book>
        {
            new Book("Обломов", "И.А.Гончаров", 67496739, 1859),
            new Book("Мертвые души", "Н.В.Гоголь", 67393062, 1842),
            new Book("Война и мир", "Л.Н.Толстой", 68396730, 1867),
            new Book("Преступление и наказание", "Ф.М.Достоевский", 78838277, 1866),
            new Book("Братья Карамазовы", "Ф.М.Достоевский", 38698289, 1880),
            new Book("Бесы", "Ф.М.Достоевский", 38584018, 1872),
            new Book("Вешние воды", "И.С.Тургенев", 58295749, 1872),
            new Book("Анна Каренина", "Л.Н.Толстой", 68375938, 1873),
            new Book("Шинель", "Н.В.Гоголь", 48395673, 1842),
        };

        var writer = new StreamWriter(filePath);
        writer.WriteLine("Название;Автор;Количество экземпляров;Год издания");

        foreach (var book in books)
        {
            writer.WriteLine($"{book.Title};{book.Author};{book.Copies};{book.Year}");
        }

        writer.Close();
    }

    static List<Book> ReadLibraryData(string filePath)
    {
        var books = new List<Book>();

        var reader = new StreamReader(filePath);
        reader.ReadLine();

        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            var values = line.Split(';');

            string title = values[0];
            string author = values[1];
            double copies = Convert.ToDouble(values[2]);
            double year = Convert.ToDouble(values[3]);

            books.Add(new Book(title, author, copies, year));
        }

        reader.Close();

        return books;
    }

    static void DisplayBooksByAuthor(List<Book> books, string author)
    {
        var booksByAuthor = books.Where(b => b.Author.Equals(author, StringComparison.OrdinalIgnoreCase)).ToList();

        if (booksByAuthor.Count > 0)
        {
            Console.WriteLine();
            Console.WriteLine($"Книги автора {author}:");
            foreach (var book in booksByAuthor)
            {
                Console.WriteLine($"Название: {book.Title}; Количество экземпляров: {book.Copies}; Год издания: {book.Year}");
            }
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine($"Книг автора {author} не найдено.");
        }
    }

    static double CountCopiesByYear(List<Book> books, double year)
    {
        return books.Where(b => b.Year == year).Sum(b => b.Copies);
    }

    static void Main(string[] args)
    {
        string desktopPath = Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), "Desktop");
        string filePath = Path.Combine(desktopPath, "Картотека.csv");

        GenerateLibraryData(filePath);

        var books = ReadLibraryData(filePath);

        var authors = new List<string>();

        foreach (var book in books)
        {
            if (!authors.Contains(book.Author))
            {
                authors.Add(book.Author);
            }
        }

        authors.Sort();

        Console.WriteLine("Доступные авторы:");

        for (int authorIndex = 0; authorIndex < authors.Count; ++authorIndex)
        {
            Console.WriteLine(authors[authorIndex]);
        }

        var years = new List<double>();

        foreach (var book in books)
        {
            if (!years.Contains(book.Year))
            {
                years.Add(book.Year);
            }
        }

        years.Sort();

        Console.WriteLine();
        Console.WriteLine("Доступные года:");

        for (int yearIndex = 0; yearIndex < years.Count; ++yearIndex)
        {
            Console.WriteLine(years[yearIndex]);
        }
        
        Console.WriteLine();
        Console.Write("Введите автора: ");
        string author = Console.ReadLine();
                
        Console.Write("Введите год издания: ");
        double year = Convert.ToDouble(Console.ReadLine());

        DisplayBooksByAuthor(books, author);
                
        double totalCopies = CountCopiesByYear(books, year);
        Console.WriteLine();
        Console.WriteLine($"Общее количество экземпляров книг, выпущенных в {year} году: {totalCopies}");

        Console.ReadKey();
        File.Delete(filePath);
    }
}