using LibraryBookManagementKWB.Models;
using LibraryBookManagementKWB.Repositories.Interfaces;

namespace LibraryBookManagementKWB.Repositories
{
    /// <summary>
    /// Implementation of the ILibraryBookRepository for managing & storing library books In-Memory.
    /// </summary>
    public class LibraryBookRepository : ILibraryBookRepository
    {
        private readonly List<LibraryBook> _libraryBooks = new();   

        public List<LibraryBook> GetAllBooks()
        {
            return _libraryBooks;
        }

        public void AddNewBook(LibraryBook book)
        {
            _libraryBooks.Add(book);
        }

        public void UpdateExistingBook(LibraryBook book)
        {
            var existingBook = GetBookByISBN(book.ISBN);
            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
            }
            else
                throw new KeyNotFoundException($"Book with ISBN: {book.ISBN} not found.");
        }

        public void DeleteExistingBook(string isbn)
        {
            var existingBook = GetBookByISBN(isbn);
            if (existingBook != null)
            {
                _libraryBooks.Remove(existingBook);
            }
            else
                throw new KeyNotFoundException($"Book with ISBN: {isbn} not found.");
        }                

        public LibraryBook? GetBookByISBN(string isbn)
        {
            var existingBook = _libraryBooks.FirstOrDefault(b => b.ISBN == isbn);
            if (existingBook != null)
            {
                return existingBook;
            }

            return null;
        }
    }
}
