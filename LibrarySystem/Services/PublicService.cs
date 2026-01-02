using System.Collections.Generic;
using LibrarySystem.Models;
using LibrarySystem.Repositories;

namespace LibrarySystem.Services
{
	public class PublicService
	{
		private BookRepository bookRepository;
		private UserRepository userRepository;

		public PublicService()
		{
			bookRepository = new BookRepository();
			userRepository = new UserRepository();
		}

		public List<Book> SearchBooks(string criteria)
		{
			return bookRepository.Search(criteria);
		}

		public Book ViewBookDetails(int bookId)
		{
			return bookRepository.GetById(bookId);
		}

		public Member RegisterMember(Member member)
		{
			userRepository.Add(member);
			return member;
		}
	}
}