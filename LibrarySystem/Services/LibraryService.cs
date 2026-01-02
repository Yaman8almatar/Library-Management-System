using System;
using System.Collections.Generic;
using LibrarySystem.Models;
using LibrarySystem.Repositories;

namespace LibrarySystem.Services
{
    public class LibraryService
    {
        // تعريف المستودعات التي يعتمد عليها السيرفس
        private UserRepository _userRepo;
        private BookRepository _bookRepo;
        private LoanRepository _loanRepo;
        private ReservationRepository _resRepo;
        private FineRepository _fineRepo;

        // Constructor لتهيئة المستودعات
        public LibraryService()
        {
            _userRepo = new UserRepository();
            _bookRepo = new BookRepository();
            _loanRepo = new LoanRepository();
            _resRepo = new ReservationRepository();
            _fineRepo = new FineRepository();
        }

        // ==========================================================
        // UC-001: تسجيل الدخول (Login)
        // ==========================================================
        public User Login(string username, string password)
        {
            User user = _userRepo.GetByUsername(username);

            // في الواقع يجب استخدام Hashing، هنا مقارنة نصية للتجربة
            if (user != null && user.PasswordHash == password)
            {
                return user;
            }
            return null; // فشل الدخول
        }

        // ==========================================================
        // UC-003: استعارة كتاب (Borrow Book)
        // ==========================================================
        public string BorrowBook(int memberId, int bookId)
        {
            try
            {
                // 1. التحقق من توفر الكتاب
                Book book = _bookRepo.GetById(bookId);
                if (book == null) return "الكتاب غير موجود.";
                if (!book.IsAvailable()) return "الكتاب غير متاح حالياً.";

                // 2. إنشاء كائن الاستعارة
                Loan newLoan = new Loan
                {
                    UserId = memberId,
                    BookId = bookId,
                    StartDate = DateTime.Now,
                    DueDate = DateTime.Now.AddDays(14), // مدة 14 يوم
                    Status = "Active"
                };

                // 3. الحفظ في قاعدة البيانات
                _loanRepo.Create(newLoan);

                // 4. إنقاص الكمية وتحديث حالة الكتاب
                _bookRepo.DecreaseAvailable(bookId);

                // تحديث الحالة إذا نفدت النسخ (اختياري، لأن DecreaseAvailable يعالج الرقم)
                if (book.AvailableCopies - 1 <= 0)
                {
                    _bookRepo.UpdateStatus(bookId, "Borrowed");
                }

                return "تمت الاستعارة بنجاح.";
            }
            catch (Exception ex)
            {
                return $"حدث خطأ: {ex.Message}";
            }
        }

        // ==========================================================
        // UC-004: إرجاع كتاب (Return Book)
        // ==========================================================
        public string ReturnBook(int loanId)
        {
            try
            {
                // 1. جلب بيانات الاستعارة
                Loan loan = _loanRepo.GetById(loanId);
                if (loan == null) return "بيانات الاستعارة غير موجودة.";
                if (loan.Status == "Closed") return "تم إرجاع هذا الكتاب مسبقاً.";

                // 2. التحقق من الغرامات (إذا تجاوز تاريخ الاستحقاق)
                if (DateTime.Now > loan.DueDate)
                {
                    // حساب الفرق بالأيام
                    int overdueDays = (DateTime.Now - loan.DueDate).Days;
                    decimal fineAmount = overdueDays * 1.0m; // مثلاً دولار عن كل يوم

                    // تسجيل الغرامة
                    Fine newFine = new Fine
                    {
                        LoanId = loanId,
                        Amount = fineAmount,
                        PaymentStatus = "Unpaid"
                    };
                    _fineRepo.Create(newFine);
                }
                _loanRepo.CloseLoan(loanId, DateTime.Now);

                // 4. زيادة نسخ الكتاب (إعادته للمخزن)
                _bookRepo.IncreaseAvailable(loan.BookId);
                _bookRepo.UpdateStatus(loan.BookId, "Available");

                return "تم إرجاع الكتاب بنجاح.";
            }
            catch (Exception ex)
            {
                return $"فشل الإرجاع: {ex.Message}";
            }
        }

        // ==========================================================
        // UC-005: تجديد الاستعارة (Renew Loan)
        // ==========================================================
        public string RenewLoan(int loanId)
        {
            Loan loan = _loanRepo.GetById(loanId);
            if (loan == null) return "الاستعارة غير موجودة.";

            // إضافة 7 أيام مثلاً
            DateTime newDueDate = loan.DueDate.AddDays(7);

            _loanRepo.UpdateDueDate(loanId, newDueDate);

            return $"تم تجديد الاستعارة. الموعد الجديد: {newDueDate.ToShortDateString()}";
        }

        // ==========================================================
        // UC-006: حجز كتاب (Reserve Book)
        // ==========================================================
        public string ReserveBook(int memberId, int bookId)
        {
            // يمكن إضافة منطق للتحقق هل الكتاب مستعار حالياً أم لا
            Reservation res = new Reservation
            {
                UserId = memberId,
                BookId = bookId,
                ResDate = DateTime.Now,
                Status = "Active"
            };

            _resRepo.Create(res);
            return "تم حجز الكتاب بنجاح.";
        }

        // ==========================================================
        // UC-007: دفع غرامة (Pay Fine)
        // ==========================================================
        public string PayFine(int fineId)
        {
            _fineRepo.UpdateStatus(fineId, "Paid");
            return "تم دفع الغرامة بنجاح.";
        }
    }
}
// 3. إغلاق الاستعارة