using LibraryBookManagementKWB.Models;

namespace LibraryBookManagementKWB.Services.Interfaces
{
    /// <summary>
    /// Definition of the ILibraryBookService for managing library books.
    /// </summary>
    public interface ILibraryBookService
    {
        List<LibraryBook> GetAllBooks();
        void AddNewBook(LibraryBook book);
        void UpdateExistingBook(LibraryBook book);
        void DeleteExistingBook(string isbn);
        LibraryBook? GetBookByISBN(string isbn);
    }
}
