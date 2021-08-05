using System;
using System.Linq;
using WebApi.BookOperations.GetBooks;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetById
{
    public class GetBookDetailQuery
    {
        public int BookId { get; set; }
        private readonly BookStoreDbContext _context;

        public GetBookDetailQuery(BookStoreDbContext context, int id)
        {
            _context = context;
            BookId = id;
        }

        public BooksViewModel Handle()
        {
            var book = _context.Books.Where(book => book.Id == BookId).SingleOrDefault();
            if (book == null)
                throw new InvalidOperationException("Kitap Bulunamadı");

            return new BooksViewModel { 
                Genre = ((GenreEnum)book.GenreId).ToString(), 
                PageCount = book.PageCount, 
                PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy"), 
                Title = book.Title };
        }
    }

    // public class BooksViewModel
    // {
    //     public string Title { get; set; }
    //     public int PageCount { get; set; }
    //     public string PublishDate { get; set; }
    //     public string Genre { get; set; }
    // }
}