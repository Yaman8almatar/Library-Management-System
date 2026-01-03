using LibrarySystem.Models;
using LibrarySystem.Repositories;
using System.Collections.Generic;

namespace LibrarySystem.Services
{
    public class PublicService
    {
        private readonly BookRepository _bookRepo;
        private readonly UserRepository _userRepo;

        public PublicService()
        {
            _bookRepo = new BookRepository();
            _userRepo = new UserRepository();
        }

        public List<Book> SearchBooks(string title)
        {
            return _bookRepo.SearchByTitle(title);
        }

        public List<Book> GetAllBooks()
        {
            return _bookRepo.GetAll();
        }

        public Book GetBookById(int bookId)
        {
            return _bookRepo.GetById(bookId);
        }

        public void RegisterMember(Member member)
        {
            // لاحقاً: هش كلمة السر هنا قبل الحفظ
            _userRepo.Add(member);
        }
    }
}