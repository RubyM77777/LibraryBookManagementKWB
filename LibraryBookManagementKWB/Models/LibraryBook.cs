
namespace LibraryBookManagementKWB.Models
{
    /// <summary>
    /// Represents a Book in the library management system.
    /// </summary>
    public class LibraryBook
    {
        public required string ISBN { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
    }
}
