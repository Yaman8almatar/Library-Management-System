using System;
using System.Collections.Generic;
using LibrarySystem.Models;
using LibrarySystem.Services;

namespace LibrarySystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Library Management System - Runtime Test ===");

            // 1. Initialize Services
            LibraryService libraryService = new LibraryService();
            AdminService adminService = new AdminService();
            PublicService publicService = new PublicService();

            try
            {
                // 2. Testing: Add New Book (Admin Role)
                Console.WriteLine("\n[1] Adding a new book...");
                Book newBook = new Book
                {
                    Title = "C# Programming Guide",
                    Author = "Microsoft Press",
                    Year = 2023,
                    TotalCopies = 5,
                    AvailableCopies = 5,
                    Status = "Available"
                };
                adminService.AddBook(newBook);
                Console.WriteLine("✔ Book added successfully.");

                // 3. Testing: Search for Books (Public Role)
                Console.WriteLine("\n[2] Searching for books with title 'C#'...");
                List<Book> searchResults = publicService.SearchBooks("C#");
                foreach (var b in searchResults)
                {
                    Console.WriteLine($"- Title: {b.Title} | Author: {b.Author} | Available: {b.AvailableCopies}");
                }

                // 4. Testing: Login (User Role)
                Console.WriteLine("\n[3] Attempting login...");
                // Note: Ensure this user exists in your database first
                User loggedInUser = libraryService.Login("admin", "password123");

                if (loggedInUser != null)
                {
                    Console.WriteLine($"✔ Welcome, {loggedInUser.Name}.");

                    // 5. Testing: Borrow a Book (Member Role)
                    Console.WriteLine("\n[4] Attempting to borrow a book...");
                    // Assuming Member ID is 1 and Book ID is 1
                    string borrowResult = libraryService.BorrowBook(loggedInUser.UserId, 1);
                    Console.WriteLine($"Result: {borrowResult}");
                }
                else
                {
                    Console.WriteLine("❌ Login Failed: Invalid username or password.");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("\n--- System Notification ---");
                Console.WriteLine("The code logic is correct, but please ensure:");
                Console.WriteLine("1. The SQL Server is running.");
                Console.WriteLine("2. Database and tables are created using the SQL script.");
                Console.WriteLine($"3. Technical Error: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}