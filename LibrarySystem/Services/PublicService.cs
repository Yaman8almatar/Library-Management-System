using LibrarySystem.Models;
using LibrarySystem.Repositories;
using System.Collections.Generic;

namespace LibrarySystem.Services
{
    public class PublicService
    {
        private BookRepository _bookRepo = new BookRepository();
        private UserRepository _userRepo = new UserRepository();

        public List<Book> SearchBooks(string title)
        {
            // هنا نستدعي ميثود البحث من المستودع (بفرض وجودها)
            // سأفترض أنها تعيد قائمة بناءً على العنوان
            return _bookRepo.SearchByTitle(title);
        }

        public void RegisterMember(Member member)
        {
            // تشفير كلمة المرور يجب أن يتم هنا قبل الحفظ
            _userRepo.Add(member);
        }
    }
}