using System;
using System.Collections.Generic;
using LibrarySystem.Models;
using LibrarySystem.Data;
using LibrarySystem.Repositories;

namespace LibrarySystem.Services
{
    public class LibraryService
    {
        private UserRepository _userRepo;
        private BookRepository _bookRepo;
        private LoanRepository _loanRepo;
        private ReservationRepository _resRepo;
        private FineRepository _fineRepo;

        public LibraryService()
        {
            _userRepo = new UserRepository();
            _bookRepo = new BookRepository();
            _loanRepo = new LoanRepository();
            _resRepo = new ReservationRepository();
            _fineRepo = new FineRepository();
        }

        // ==========================
        // LOGIN
        // ==========================
        public User Login(string username, string password)
        {
            User user = _userRepo.GetByUsername(username);
            if (user != null && user.PasswordHash == password)
                return user;
            return null;
        }

        // ==========================
        // SEARCH BOOKS
        // ==========================
        public List<Book> SearchBooks(string title)
        {
            return _bookRepo.SearchByTitle(title);
        }

        // ==========================
        // BORROW BOOK
        // ==========================
        public string BorrowBook(int memberId, int bookId)
        {
            Book book = _bookRepo.GetById(bookId);
            if (book == null) return "Book not found.";
            if (!book.IsAvailable()) return "Book is not available.";

            Loan loan = new Loan
            {
                UserId = memberId,
                BookId = bookId,
                StartDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(14),
                Status = "Active"
            };

            _loanRepo.Create(loan);
            _bookRepo.DecreaseAvailable(bookId);

            if (book.AvailableCopies - 1 <= 0)
                _bookRepo.UpdateStatus(bookId, "Borrowed");

            return "Book borrowed successfully.";
        }

        // ==========================
        // RETURN BOOK
        // ==========================
        public string ReturnBook(int loanId)
        {
            Loan loan = _loanRepo.GetById(loanId);
            if (loan == null) return "Loan not found.";
            if (loan.Status == "Closed") return "Book already returned.";

            if (DateTime.Now > loan.DueDate)
            {
                int overdueDays = (DateTime.Now - loan.DueDate).Days;
                decimal fineAmount = overdueDays * 1.0m;
                Fine fine = new Fine { LoanId = loanId, Amount = fineAmount, PaymentStatus = "Unpaid" };
                _fineRepo.Create(fine);
            }

            _loanRepo.CloseLoan(loanId, DateTime.Now);
            _bookRepo.IncreaseAvailable(loan.BookId);
            _bookRepo.UpdateStatus(loan.BookId, "Available");

            return "Book returned successfully.";
        }

        // ==========================
        // RENEW LOAN
        // ==========================
        public string RenewLoan(int loanId)
        {
            Loan loan = _loanRepo.GetById(loanId);
            if (loan == null) return "Loan not found.";

            DateTime newDue = loan.DueDate.AddDays(7);
            _loanRepo.UpdateDueDate(loanId, newDue);
            return $"Loan renewed. New due date: {newDue.ToShortDateString()}";
        }

        // ==========================
        // RESERVE BOOK
        // ==========================
        public string ReserveBook(int memberId, int bookId)
        {
            Reservation res = new Reservation
            {
                UserId = memberId,
                BookId = bookId,
                ResDate = DateTime.Now,
                Status = "Pending"
            };

            _resRepo.Add(res);
            return "Book reserved successfully.";
        }

        // ==========================
        // PAY FINE// ==========================
        public string PayFine(int fineId)
        {
            _fineRepo.UpdateStatus(fineId, "Paid");
            return "Fine has been paid successfully.";
        }

        // ==========================
        // HELPER METHODS
        // ==========================
        public List<Loan> GetLoansByUser(int userId)
        {
            return _loanRepo.GetByUser(userId);
        }

        public List<Fine> GetFinesByUser(int userId)
        {
            return _fineRepo.GetByUser(userId);
        }

        public void DeleteBook(int bookId)
        {
            _bookRepo.Delete(bookId);
        }

        public void UpdateBook(Book book)
        {
            _bookRepo.Update(book);
        }

        public void UpdateUser(User user)
        {
            _userRepo.Update(user);
        }
    }
}