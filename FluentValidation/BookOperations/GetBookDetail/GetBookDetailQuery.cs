using System;
using System.Linq;
using AutoMapper;
using WebApi.BookOperations.GetBooks;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {
        public int BookId { get; set; }
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBookDetailQuery(BookStoreDbContext context, int id, IMapper mapper)
        {
            _context = context;
            BookId = id;
            _mapper = mapper;
        }

        public BooksDetailViewModel Handle()
        {
            var book = _context.Books.Where(book => book.Id == BookId).SingleOrDefault();
            if (book == null)
                throw new InvalidOperationException("Kitap Bulunamadı");

            return _mapper.Map<BooksDetailViewModel>(book);
        }
    }

    public class BooksDetailViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }
}