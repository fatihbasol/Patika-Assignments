using System;
using System.Linq;
using System.Text.Json.Serialization;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Common;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.BookOperations.Queries.GetBookDetail
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
            var book = _context.Books.Include(x => x.Genre).Include(x => x.Author).Where(book => book.Id == BookId).SingleOrDefault();
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
        //public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}