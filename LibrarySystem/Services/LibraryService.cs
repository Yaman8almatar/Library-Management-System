using System.Collections.Generic;
using LibrarySystem.Models;
using LibrarySystem.Repositories;

namespace LibrarySystem.Services
{
	public class LibraryService
	{
		private UserRepository userRepository;
		private BookRepository bookRepository;
		private LoanRepository loanRepository;
		private ReservationRepository reservationRepository;
		private FineRepository fineRepository;

		public LibraryService()
		{
			userRepository = new UserRepository();
			bookRepository = new BookRepository();
			loanRepository = new LoanRepository();
			reservationRepository = new ReservationRepository();
			fineRepository = new FineRepository();
		}

		public User Login(string username, string password)
		{
			return userRepository.GetByUsername(username);
		}

		public void Logout()
		{
			// Session handling (future)
		}

		public Loan BorrowBook(int memberId, int bookId)
		{
			Book book = bookRepository.GetById(bookId);

			if (book == null || !book.IsAvailable())
				return null;

			Loan loan = new Loan();
			loanRepository.Create(loan);
			bookRepository.DecreaseAvailable(bookId);

			return loan;
		}

		public void ReturnBook(int loanId)
		{
			Loan loan = loanRepository.GetById(loanId);
			loanRepository.Update(loan);
			bookRepository.IncreaseAvailable(loan.BookId);
		}

		public Loan RenewLoan(int loanId)
		{
			Loan loan = loanRepository.GetById(loanId);
			loanRepository.Update(loan);
			return loan;
		}

		public Reservation ReserveBook(int memberId, int bookId)
		{
			Reservation reservation = new Reservation();
			return reservationRepository.Create(reservation);
		}

		public void PayFine(int fineId)
		{
			Fine fine = fineRepository.GetByLoan(fineId);
			fine.MarkAsPaid();
			fineRepository.Update(fine);
		}

		public List<Loan> ViewLoanHistory(int memberId)
		{
			return loanRepository.GetActiveByMember(memberId);
		}
	}
}