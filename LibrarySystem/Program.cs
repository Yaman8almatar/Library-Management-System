using System;
using LibrarySystem.Models;
using LibrarySystem.Services;

namespace LibrarySystem
{
    class Program
    {
        static void Main(string[] args)
        {
            PublicService publicService = new PublicService();
            AdminService adminService = new AdminService();

            // 1. Register a new member
            Member newMember = new Member
            {
                Name = "John Doe",
                Username = "johnd",
                PasswordHash = "1234",
                Email = "john@example.com",
                JoinDate = DateTime.Now
            };
            publicService.RegisterMember(newMember);

            // 2. Add a new book
            Book newBook = new Book
            {
                Title = "C# in Depth",
                Author = "Jon Skeet",
                Year = 2023,
                TotalCopies = 3,
                AvailableCopies = 3,
                Status = "Available"
            };
            adminService.AddBook(newBook);

            // 3. Search for books
            var books = publicService.SearchBooks("C#");
            Console.WriteLine("Search results:");
            foreach (var b in books)
            {
                Console.WriteLine($"- {b.Title} by {b.Author} ({b.AvailableCopies} available)");
            }

            // 4. List all users
            var users = adminService.GetAllUsers();
            Console.WriteLine("All Users:");
            foreach (var u in users)
            {
                Console.WriteLine($"- {u.UserId}: {u.Name} ({u.UserType})");
            }

            Console.WriteLine("Test completed.");
        }
    }
}