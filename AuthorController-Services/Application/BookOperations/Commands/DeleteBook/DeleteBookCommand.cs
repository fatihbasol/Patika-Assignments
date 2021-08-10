using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommand
    {
        public int BookId { get; set; }
        private readonly BookStoreDbContext _dbContext;

        public DeleteBookCommand(BookStoreDbContext context, int bookId)
        {
            _dbContext = context;
            BookId = bookId;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
            if (book is null)
                throw new InvalidOperationException("Kitap bulunamadı");

            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();

        }
    }
}