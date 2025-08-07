using LibraryBookManagementKWB.Models;
using LibraryBookManagementKWB.Services.Interfaces;

namespace LibraryBookManagementKWB.UIConsole
{
    /// <summary>
    /// LibraryMenu class provides a console-based UI for managing library books by performing CRUD operations.
    /// </summary>
    public class LibraryMenu
    {
        private readonly ILibraryBookService _libraryBookService;
        public LibraryMenu(ILibraryBookService libraryBookService)
        {
            _libraryBookService = libraryBookService;
        }
        public void DisplayLibraryMenu()
        {
            bool isFirstTime = true;
            while (true)
            {
                if (isFirstTime)
                {
                    Console.WriteLine("\nWELCOME TO LIBRARY MANAGEMENT SYSTEM");
                    Console.WriteLine("1. Add a new book");
                    Console.WriteLine("2. Update an existing book");
                    Console.WriteLine("3. Delete a book");
                    Console.WriteLine("4. List all books");
                    Console.WriteLine("5. View details of a specific book");
                    Console.WriteLine("6. Exit");
                    isFirstTime = false;
                }
                Console.Write("\nPlease select an option: ");

                var option = Console.ReadLine();

                try
                {
                    switch (option)
                    {
                        case "1": AddNewBook(); break;
                        case "2": UpdateExistingBook(); break;
                        case "3": DeleteExistingBook(); break;
                        case "4": GetAllBooks(); break;
                        case "5": GetBookByISBN(); break;
                        case "6": return;
                        default: Console.WriteLine("Invalid option. Please select a valid option."); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }


        private void AddNewBook()
        {
            Console.Write("\nEnter ISBN: ");
            var isbn = Console.ReadLine();
            Console.Write("Enter Title: ");
            var title = Console.ReadLine();
            Console.Write("Enter Author: ");
            var author = Console.ReadLine();
            var book = new LibraryBook { ISBN = isbn!, Title = title!, Author = author! };

            _libraryBookService.AddNewBook(book);
        }

        private void UpdateExistingBook()
        {
            Console.Write("\nEnter ISBN to update: ");
            var isbn = Console.ReadLine();
            var book = _libraryBookService.GetBookByISBN(isbn!);
            if (book == null)
            {
                Console.WriteLine("BOOK NOT FOUND.");
                return;
            }
            Console.Write("Enter new Title: ");
            book.Title = Console.ReadLine()!;
            Console.Write("Enter new Author: ");
            book.Author = Console.ReadLine()!;
            _libraryBookService.UpdateExistingBook(book);
        }

        private void DeleteExistingBook()
        {
            Console.Write("\nEnter ISBN to delete: ");
            var isbn = Console.ReadLine();
            _libraryBookService.DeleteExistingBook(isbn!);
        }

        private void GetAllBooks()
        {
            var books = _libraryBookService.GetAllBooks();
            if (books.Count == 0)
            {
                Console.WriteLine("NO BOOKS AVAILABLE.");
                return;
            }
            Console.WriteLine("List of all books:");
            foreach (var book in books)
            {
                Console.WriteLine($"ISBN: {book.ISBN}, Title: {book.Title}, Author: {book.Author}");
            }
        }

        private void GetBookByISBN()
        {
            Console.Write("\nEnter ISBN to view the details: ");
            var isbn = Console.ReadLine();
            var book = _libraryBookService.GetBookByISBN(isbn!);
            if (book == null)
            {
                Console.WriteLine("BOOK NOT FOUND.");
                return;
            }
            Console.WriteLine($"ISBN: {book.ISBN}, Title: {book.Title}, Author: {book.Author}");
        }
    }
}
