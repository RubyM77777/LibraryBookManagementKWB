using LibraryBookManagementKWB.Models;

namespace LibraryBookManagementKWB.Repositories.Interfaces
{
    /// <summary>
    /// Definition of the ILibraryBookRepository for managing & storing library books In-Memory.
    /// </summary>
    public interface ILibraryBookRepository
    {
        List<LibraryBook> GetAllBooks();
        void AddNewBook(LibraryBook book);
        void UpdateExistingBook(LibraryBook book);
        void DeleteExistingBook(string isbn);
        LibraryBook? GetBookByISBN(string isbn);
    }
}
