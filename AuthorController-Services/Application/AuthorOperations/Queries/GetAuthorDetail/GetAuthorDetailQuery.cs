using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        public int AuthorId { get; set; }
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorDetailQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public AuthorDetailViewModel Handle()
        {
            var author = _context.Authors.Include(x => x.Books).SingleOrDefault(x => x.Id == AuthorId);

            if (author == null)
                throw new InvalidOperationException("Yazar bulunamadı.");

            var returnObj = _mapper.Map<AuthorDetailViewModel>(author);
            return returnObj;

        }

    }

    public class AuthorDetailViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FullName { get { return Name + " " + Surname; } }
        public DateTime BirthDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public List<Book> Books { get; set; }
    }

    // public class AuthorDetailBooksViewModel
    // {
    //     public string Title { get; set; }
    //     public int PageCount { get; set; }
    //     public string PublishDate { get; set; }
    //     public string Genre { get; set; }
    //     public string Author { get; set; }

    // }
}