using LibraryBookManagementKWB.Repositories;
using LibraryBookManagementKWB.Repositories.Interfaces;
using LibraryBookManagementKWB.Services;
using LibraryBookManagementKWB.Services.Interfaces;
using LibraryBookManagementKWB.UIConsole;
using Microsoft.Extensions.DependencyInjection;


// Register services and repositories in the DI container
var services = new ServiceCollection();
services.AddSingleton<ILibraryBookRepository, LibraryBookRepository>();
services.AddSingleton<ILibraryBookService, LibraryBookService>();
services.AddSingleton<LibraryMenu>();

// Build the service provider to display the LibraryMenu in the Console
var provider = services.BuildServiceProvider();
var menu = provider.GetRequiredService<LibraryMenu>();
menu.DisplayLibraryMenu();
