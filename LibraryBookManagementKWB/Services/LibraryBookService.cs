using LibraryBookManagementKWB.Models;
using LibraryBookManagementKWB.Repositories.Interfaces;
using LibraryBookManagementKWB.Services.Interfaces;

namespace LibraryBookManagementKWB.Services
{
    /// <summary>
    /// Implementation of the ILibraryBookService using ILibraryBookRepository for managing library books.
    /// Performed CRUD operations, Book & ISBN Validations, Exception Handling,Dependency Injection.
    /// </summary>
    public class LibraryBookService : ILibraryBookService
    {
        private readonly ILibraryBookRepository _libraryBookRepository;

        public LibraryBookService(ILibraryBookRepository libraryBookRepository)
        {
            _libraryBookRepository = libraryBookRepository;
        }

        public List<LibraryBook> GetAllBooks()
        {
            try
            {
                return _libraryBookRepository.GetAllBooks();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving all books: {ex.Message}");
                return new List<LibraryBook>();
            }
        }

        public void AddNewBook(LibraryBook book)
        {
            try
            {
                ValidateLibraryBook(book);

                if (GetBookByISBN(book.ISBN) != null)
                {
                    Console.WriteLine($"A book with ISBN: {book.ISBN} already exists.");
                    return;
                }

                _libraryBookRepository.AddNewBook(book);
                Console.WriteLine("LIBRARY BOOK ADDED.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding book: {ex.Message}");
            }
        }

        public void UpdateExistingBook(LibraryBook book)
        {
            try
            {
                ValidateLibraryBook(book);

                if (GetBookByISBN(book.ISBN) == null)
                {
                    Console.WriteLine($"Book with ISBN: {book.ISBN} not found.");
                    return;
                }

                _libraryBookRepository.UpdateExistingBook(book);
                Console.WriteLine("LIBRARY BOOK UPDATED.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating book: {ex.Message}");
            }
        }

        public void DeleteExistingBook(string isbn)
        {
            try
            {
                if (GetBookByISBN(isbn) == null)
                {
                    Console.WriteLine($"Book with ISBN: {isbn} not found.");
                    return;
                }

                _libraryBookRepository.DeleteExistingBook(isbn);
                Console.WriteLine("LIBRARY BOOK DELETED.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting book: {ex.Message}");
            }
        }

        public LibraryBook? GetBookByISBN(string isbn)
        {
            try
            {
                return _libraryBookRepository.GetBookByISBN(isbn);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving book: {ex.Message}");
                return null;
            }
        }

        private void ValidateLibraryBook(LibraryBook book)
        {
            //Validate the Book and its properties. Throw exceptions if validation fails.
            if (book == null)
                throw new ArgumentNullException(nameof(book), "Book cannot be null.");

            if (string.IsNullOrWhiteSpace(book.ISBN))
                throw new ArgumentException("ISBN cannot be empty.", nameof(book.ISBN));

            if (book.ISBN.Length != 13 || !book.ISBN.All(char.IsDigit))
                throw new ArgumentException("ISBN must be a 13-digit number.", nameof(book.ISBN));

            if (string.IsNullOrWhiteSpace(book.Title))
                throw new ArgumentException("Title cannot be empty.", nameof(book.Title));

            if (string.IsNullOrWhiteSpace(book.Author))
                throw new ArgumentException("Author cannot be empty.", nameof(book.Author));
        }
    }
}
