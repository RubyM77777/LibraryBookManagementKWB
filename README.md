# LibraryManagementSystemKWB

**Assignment: Library Management System**

**Objective:**

  Create a simple console application written in C# .NET Core for managing a library of
  books. The primary focus should be on demonstrating clean code practices, separation of
  concerns, and correct implementation of the Repository Pattern. Please do not spend more
  than an hour on the assignment; it is designed to assess the candidate’s ability to meet the
  requirements without over-engineering.

**Requirements:**
  1. Concrete Repository Implementations:
  o Implement the repository interfaces.
  o Store data in an in-memory data structure for simplicity.
  
  2. Service Layer:
  o Create a service layer that uses the repositories to perform operations.
  o The service layer should handle business logic and validation you must
  validate 13-digit ISBN format.
  
  3. Console Application:
  o Implement a simple console interface to interact with the system.
  o Provide options to:
  ▪ Add a new book
  ▪ Update an existing book
  ▪ Delete a book
  ▪ List all books
  ▪ View details of a specific book
  
  4. Unit Tests:
  o Write unit tests for the repository and service layers.


**Implementation:**

LibraryBookManagementKWB/
│
├── Models/
│   └── LibraryBook.cs
│
├── Repositories/
│   ├── Interfaces/
│   │   ├── ILibraryBookRepository.cs
│   └── LibraryBookRepository.cs
│
├── Services/
│   ├── Interfaces/
│   │   ├── ILibraryBookService.cs
│   └── LibraryBookService.cs
│
├── UI/
│   └── LibraryMenu.cs
│
├── Program.cs
│
└── Tests/
    └── LibraryBookServiceUnitTests.cs


1. Created New Project -> Console App (.NET 8). Added all required folders, files & xUnit tests project.
2. Added LibraryBook Model with its properties. 
3. Added LibraryBookRepository with Interface following SoC & SOLID.
4. Added LibraryBookService with Interface (service layer) following SoC & SOLID.
5. Defined & Implemented all the Methods in LibraryBookRepository. Used List for managing & storing library books In-Memory.
6. Installed DI package. Registered Services & Repositories in the DI container. Added Constructor Injection to call the LibraryBookRepository Methods in the Service layer.
7. Defined & Implemented all the Methods in LibraryBookService. Implemented all CRUD Operations for Library Books.
8. Added Book & ISBN Validations to handle exceptions.
9. Implemented Try-Catch Exception Handling to handle all the exceptions and show meaningful messages to the user.
10. Created Console UI LibraryMenu with 6 menu options to perform CRUD Operations.
11. Installed Moq package & written positive & negative Unit Tests for both Fact & Theory Cases.
12. Added comments wherever neccessary & updated README.md file.

