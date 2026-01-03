using LibrarySystem.Models;
using LibrarySystem.Repositories;
using System.Collections.Generic;

namespace LibrarySystem.Services
{
    public class AdminService
    {
        private readonly BookRepository _bookRepo;
        private readonly UserRepository _userRepo;

        public AdminService()
        {
            _bookRepo = new BookRepository();
            _userRepo = new UserRepository();
        }

        public void AddBook(Book book)
        {
            _bookRepo.Add(book);
        }

        public void UpdateBook(Book book)
        {
            _bookRepo.Update(book);
        }

        public void DeleteBook(int bookId)
        {
            _bookRepo.Delete(bookId);
        }

        public List<Book> GetAllBooks()
        {
            return _bookRepo.GetAll();
        }

        public void DeleteUser(int userId)
        {
            _userRepo.Delete(userId);
        }

        public List<User> GetAllUsers()
        {
            return _userRepo.GetAll();
        }
    }
}