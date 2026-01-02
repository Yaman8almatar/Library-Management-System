using LibrarySystem.Models;
using LibrarySystem.Repositories;

namespace LibrarySystem.Services
{
    public class AdminService
    {
        private UserRepository userRepository;
        private BookRepository bookRepository;

        public AdminService()
        {
            userRepository = new UserRepository();
            bookRepository = new BookRepository();
        }

        public Book AddBook(Book book)
        {
            return bookRepository.Add(book);
        }

        public void UpdateBook(Book book)
        {
            bookRepository.Update(book);
        }

        public void DeleteBook(int bookId)
        {
            bookRepository.Delete(bookId);
        }

        public void ManageUser(int userId, string action)
        {
            if (action == "delete")
                userRepository.Delete(userId);
        }
    }
}