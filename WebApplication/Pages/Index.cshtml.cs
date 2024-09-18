using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace WebApplication.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public List<Book> Books { get; set; }

        public void OnGet()
        {
            Books = new List<Book>();

            PhysicalBook physicalBook = new PhysicalBook("The Great Gatsby", " F. Scott Fitzgerald", "978-0743273565", 180);
            EBook eBook = new EBook("Pride and Prejudice", "Jane Austenl", "978-1503290563", 432);

            Books.Add(physicalBook);
            Books.Add(eBook);
        }

        public IActionResult OnPostAddBook(string title, string author, string isbn, string type, int numberOfPages = 0, int fileSize = 0)
        {
            Book book;
            if (type == "PhysicalBook")
            {
                book = new PhysicalBook(title, author, isbn, numberOfPages);
            }
            else
            {
                book = new EBook(title, author, isbn, fileSize);
            }
            Books.Add(book);
            return RedirectToPage();
        }

        public IActionResult OnPostRemoveBook(int index)
        {
            Books.RemoveAt(index);
            return RedirectToPage();
        }
    }

    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }

        public Book(string title, string author, string isbn)
        {
            Title = title;
            Author = author;
            ISBN = isbn;
        }

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Title: {Title}, Author: {Author}, ISBN: {ISBN}");
        }
    }

    public class PhysicalBook : Book
    {
        public int NumberOfPages { get; set; }

        public PhysicalBook(string title, string author, string isbn, int numberOfPages)
            : base(title, author, isbn)
        {
            NumberOfPages = numberOfPages;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Number of Pages: {NumberOfPages}");
        }
    }

    public class EBook : Book
    {
        public int FileSize { get; set; }

        public EBook(string title, string author, string isbn, int fileSize)
            : base(title, author, isbn)
        {
            FileSize = fileSize;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"File Size: {FileSize} KB");
        }
    }
}
