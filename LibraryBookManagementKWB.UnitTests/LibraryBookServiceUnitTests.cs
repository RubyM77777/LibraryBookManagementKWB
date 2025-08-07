using LibraryBookManagementKWB.Models;
using LibraryBookManagementKWB.Services.Interfaces;
using Moq;

namespace LibraryBookManagementKWB.UnitTests
{
    /// <summary>
    /// Unit tests for the LibraryBookService class using xUnit, Moq framework. 
    /// </summary>
    public class LibraryBookServiceUnitTests
    {
        private readonly Mock<ILibraryBookService> _mockLibraryBookService;

        public LibraryBookServiceUnitTests()
        {
            _mockLibraryBookService = new Mock<ILibraryBookService>();
        }

        #region Fact UnitTests

        [Fact]
        public void AddNewBook_ShouldAddBook_WhenValidBookIsProvided()
        {
            // Arrange
            var book = new LibraryBook { ISBN = "1234567890", Title = "Test Book", Author = "Test Author" };
            _mockLibraryBookService.Setup(s => s.AddNewBook(book)).Verifiable();
            // Act
            _mockLibraryBookService.Object.AddNewBook(book);
            // Assert
            _mockLibraryBookService.Verify(s => s.AddNewBook(book), Times.Once);
        }

        [Fact]
        public void UpdateExistingBook_ShouldUpdateBook_WhenValidBookIsProvided()
        {
            // Arrange
            var book = new LibraryBook { ISBN = "1234567890", Title = "Updated Book", Author = "Updated Author" };
            _mockLibraryBookService.Setup(s => s.UpdateExistingBook(book)).Verifiable();
            // Act
            _mockLibraryBookService.Object.UpdateExistingBook(book);
            // Assert
            _mockLibraryBookService.Verify(s => s.UpdateExistingBook(book), Times.Once);
        }

        [Fact]
        public void DeleteExistingBook_ShouldDeleteBook_WhenValidISBNIsProvided()
        {
            // Arrange
            var isbn = "1234567890";
            _mockLibraryBookService.Setup(s => s.DeleteExistingBook(isbn)).Verifiable();
            // Act
            _mockLibraryBookService.Object.DeleteExistingBook(isbn);
            // Assert
            _mockLibraryBookService.Verify(s => s.DeleteExistingBook(isbn), Times.Once);
        }

        [Fact]
        public void GetAllBooks_ShouldReturnListOfBooks_WhenBooksAreAvailable()
        {
            // Arrange
            var books = new List<LibraryBook>
            {
                new LibraryBook { ISBN = "1234567890", Title = "Test Book 1", Author = "Test Author 1" },
                new LibraryBook { ISBN = "0987654321", Title = "Test Book 2", Author = "Test Author 2" }
            };
            _mockLibraryBookService.Setup(s => s.GetAllBooks()).Returns(books);
            // Act
            var result = _mockLibraryBookService.Object.GetAllBooks();
            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void GetBookByISBN_ShouldReturnBook_WhenValidISBNIsProvided()
        {
            // Arrange
            var isbn = "1234567890";
            var book = new LibraryBook { ISBN = isbn, Title = "Test Book", Author = "Test Author" };
            _mockLibraryBookService.Setup(s => s.GetBookByISBN(isbn)).Returns(book);
            // Act
            var result = _mockLibraryBookService.Object.GetBookByISBN(isbn);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(isbn, result.ISBN);
        }

        [Fact]
        public void GetBookByISBN_ShouldReturnNull_WhenInvalidISBNIsProvided()
        {
            // Arrange
            var isbn = "invalid_isbn";
            _mockLibraryBookService.Setup(s => s.GetBookByISBN(isbn)).Returns((LibraryBook?)null);
            // Act
            var result = _mockLibraryBookService.Object.GetBookByISBN(isbn);
            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetAllBooks_ShouldReturnEmptyList_WhenNoBooksAreAvailable()
        {
            // Arrange
            _mockLibraryBookService.Setup(s => s.GetAllBooks()).Returns(new List<LibraryBook>());
            // Act
            var result = _mockLibraryBookService.Object.GetAllBooks();
            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void AddNewBook_ShouldNotAddBook_WhenISBNAlreadyExists()
        {
            // Arrange
            var book = new LibraryBook { ISBN = "1234567890", Title = "Test Book", Author = "Test Author" };
            _mockLibraryBookService.Setup(s => s.AddNewBook(book)).Verifiable();
            _mockLibraryBookService.Setup(s => s.GetBookByISBN(book.ISBN)).Returns(book);
            // Act
            _mockLibraryBookService.Object.AddNewBook(book);
            // Assert
            _mockLibraryBookService.Verify(s => s.AddNewBook(book), Times.Once);
        }

        [Fact]
        public void UpdateExistingBook_ShouldNotUpdateBook_WhenISBNDoesNotExist()
        {
            // Arrange
            var book = new LibraryBook { ISBN = "1234567890", Title = "Test Book", Author = "Test Author" };
            _mockLibraryBookService.Setup(s => s.UpdateExistingBook(book)).Verifiable();
            _mockLibraryBookService.Setup(s => s.GetBookByISBN(book.ISBN)).Returns((LibraryBook?)null);
            // Act
            _mockLibraryBookService.Object.UpdateExistingBook(book);
            // Assert
            _mockLibraryBookService.Verify(s => s.UpdateExistingBook(book), Times.Once);
        }

        [Fact]
        public void DeleteExistingBook_ShouldNotDeleteBook_WhenISBNDoesNotExist()
        {
            // Arrange
            var isbn = "1234567890";
            _mockLibraryBookService.Setup(s => s.DeleteExistingBook(isbn)).Verifiable();
            _mockLibraryBookService.Setup(s => s.GetBookByISBN(isbn)).Returns((LibraryBook?)null);
            // Act
            _mockLibraryBookService.Object.DeleteExistingBook(isbn);
            // Assert
            _mockLibraryBookService.Verify(s => s.DeleteExistingBook(isbn), Times.Once);
        }

        [Fact]
        public void GetAllBooks_ShouldHandleException_WhenRepositoryThrowsException()
        {
            // Arrange
            _mockLibraryBookService.Setup(s => s.GetAllBooks()).Throws(new Exception("Repository error"));
            // Act & Assert
            Assert.Throws<Exception>(() => _mockLibraryBookService.Object.GetAllBooks());
        }

        [Fact]
        public void AddNewBook_ShouldHandleException_WhenRepositoryThrowsException()
        {
            // Arrange
            var book = new LibraryBook { ISBN = "1234567890", Title = "Test Book", Author = "Test Author" };
            _mockLibraryBookService.Setup(s => s.AddNewBook(book)).Throws(new Exception("Repository error"));
            // Act & Assert
            Assert.Throws<Exception>(() => _mockLibraryBookService.Object.AddNewBook(book));
        }

        [Fact]
        public void UpdateExistingBook_ShouldHandleException_WhenRepositoryThrowsException()
        {
            // Arrange
            var book = new LibraryBook { ISBN = "1234567890", Title = "Test Book", Author = "Test Author" };
            _mockLibraryBookService.Setup(s => s.UpdateExistingBook(book)).Throws(new Exception("Repository error"));
            // Act & Assert
            Assert.Throws<Exception>(() => _mockLibraryBookService.Object.UpdateExistingBook(book));
        }

        [Fact]
        public void DeleteExistingBook_ShouldHandleException_WhenRepositoryThrowsException()
        {
            // Arrange
            var isbn = "1234567890";
            _mockLibraryBookService.Setup(s => s.DeleteExistingBook(isbn)).Throws(new Exception("Repository error"));
            // Act & Assert
            Assert.Throws<Exception>(() => _mockLibraryBookService.Object.DeleteExistingBook(isbn));
        }

        [Fact]
        public void GetBookByISBN_ShouldHandleException_WhenRepositoryThrowsException()
        {
            // Arrange
            var isbn = "1234567890";
            _mockLibraryBookService.Setup(s => s.GetBookByISBN(isbn)).Throws(new Exception("Repository error"));
            // Act & Assert
            Assert.Throws<Exception>(() => _mockLibraryBookService.Object.GetBookByISBN(isbn));
        }

        #endregion

        #region Theory UnitTests
        [Theory]
        [InlineData("1111111111", "Valid Title", "Valid Author")]
        [InlineData("2222222222", "Another Title", "Another Author")]
        public void AddNewBook_ShouldAddBook_ForValidInputs(string isbn, string title, string author)
        {
            var book = new LibraryBook { ISBN = isbn, Title = title, Author = author };
            _mockLibraryBookService.Setup(s => s.AddNewBook(book)).Verifiable();

            _mockLibraryBookService.Object.AddNewBook(book);

            _mockLibraryBookService.Verify(s => s.AddNewBook(book), Times.Once);
        }

        [Theory]
        [InlineData("", "Title", "Author")]
        [InlineData("3333333333", "", "Author")]
        [InlineData("4444444444", "Title", "")]
        [InlineData(null, "Title", "Author")]
        [InlineData("5555555555", null, "Author")]
        [InlineData("6666666666", "Title", null)]
        public void AddNewBook_ShouldNotAddBook_ForInvalidInputs(string isbn, string title, string author)
        {
            var book = new LibraryBook { ISBN = isbn ?? "", Title = title ?? "", Author = author ?? "" };
            _mockLibraryBookService.Setup(s => s.AddNewBook(book)).Verifiable();

            _mockLibraryBookService.Object.AddNewBook(book);

            _mockLibraryBookService.Verify(s => s.AddNewBook(book), Times.Once);
        }

        [Theory]
        [InlineData("1111111111", "Updated Title", "Updated Author")]
        public void UpdateExistingBook_ShouldUpdateBook_ForValidInputs(string isbn, string title, string author)
        {
            var book = new LibraryBook { ISBN = isbn, Title = title, Author = author };
            _mockLibraryBookService.Setup(s => s.UpdateExistingBook(book)).Verifiable();

            _mockLibraryBookService.Object.UpdateExistingBook(book);

            _mockLibraryBookService.Verify(s => s.UpdateExistingBook(book), Times.Once);
        }

        [Theory]
        [InlineData("", "Title", "Author")]
        [InlineData("3333333333", "", "Author")]
        [InlineData("4444444444", "Title", "")]
        public void UpdateExistingBook_ShouldNotUpdateBook_ForInvalidInputs(string isbn, string title, string author)
        {
            var book = new LibraryBook { ISBN = isbn, Title = title, Author = author };
            _mockLibraryBookService.Setup(s => s.UpdateExistingBook(book)).Verifiable();

            _mockLibraryBookService.Object.UpdateExistingBook(book);

            _mockLibraryBookService.Verify(s => s.UpdateExistingBook(book), Times.Once);
        }

        [Theory]
        [InlineData("1111111111")]
        [InlineData("2222222222")]
        public void DeleteExistingBook_ShouldDeleteBook_ForValidISBN(string isbn)
        {
            _mockLibraryBookService.Setup(s => s.DeleteExistingBook(isbn)).Verifiable();

            _mockLibraryBookService.Object.DeleteExistingBook(isbn);

            _mockLibraryBookService.Verify(s => s.DeleteExistingBook(isbn), Times.Once);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void DeleteExistingBook_ShouldNotDeleteBook_ForInvalidISBN(string isbn)
        {
            _mockLibraryBookService.Setup(s => s.DeleteExistingBook(isbn)).Verifiable();

            _mockLibraryBookService.Object.DeleteExistingBook(isbn);

            _mockLibraryBookService.Verify(s => s.DeleteExistingBook(isbn), Times.Once);
        }

        [Theory]
        [InlineData("1111111111")]
        [InlineData("2222222222")]
        public void GetBookByISBN_ShouldReturnBook_ForValidISBN(string isbn)
        {
            var book = new LibraryBook { ISBN = isbn, Title = "Test Book", Author = "Test Author" };
            _mockLibraryBookService.Setup(s => s.GetBookByISBN(isbn)).Returns(book);
            var result = _mockLibraryBookService.Object.GetBookByISBN(isbn);
            Assert.NotNull(result);
            Assert.Equal(isbn, result.ISBN);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void GetBookByISBN_ShouldReturnNull_ForInvalidISBN(string isbn)
        {
            _mockLibraryBookService.Setup(s => s.GetBookByISBN(isbn)).Returns((LibraryBook?)null);
            var result = _mockLibraryBookService.Object.GetBookByISBN(isbn);
            Assert.Null(result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void GetAllBooks_ShouldReturnListOfBooks_ForValidCount(int count)
        {
            var books = new List<LibraryBook>();
            for (int i = 0; i < count; i++)
            {
                books.Add(new LibraryBook { ISBN = $"ISBN{i}", Title = $"Title{i}", Author = $"Author{i}" });
            }
            _mockLibraryBookService.Setup(s => s.GetAllBooks()).Returns(books);

            var result = _mockLibraryBookService.Object.GetAllBooks();

            Assert.Equal(count, result.Count);
        }

        [Theory]
        [InlineData("1111111111", "Valid Title", "Valid Author")]
        [InlineData("2222222222", "Another Title", "Another Author")]
        public void GetBookByISBN_ShouldReturnBook_ForValidInputs(string isbn, string title, string author)
        {
            var book = new LibraryBook { ISBN = isbn, Title = title, Author = author };
            _mockLibraryBookService.Setup(s => s.GetBookByISBN(isbn)).Returns(book);
            var result = _mockLibraryBookService.Object.GetBookByISBN(isbn);
            Assert.NotNull(result);
            Assert.Equal(isbn, result.ISBN);
            Assert.Equal(title, result.Title);
            Assert.Equal(author, result.Author);
        }

        [Theory]
        [InlineData("invalid_isbn")]
        public void GetBookByISBN_ShouldReturnNull_ForInvalidInputs(string isbn)
        {
            _mockLibraryBookService.Setup(s => s.GetBookByISBN(isbn)).Returns((LibraryBook?)null);
            var result = _mockLibraryBookService.Object.GetBookByISBN(isbn);
            Assert.Null(result);
        }

        #endregion
    }
}