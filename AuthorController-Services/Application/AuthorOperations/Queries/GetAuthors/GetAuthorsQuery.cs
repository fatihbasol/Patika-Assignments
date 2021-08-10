using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQuery
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetAuthorsQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<AuthorViewModel> Handle()
        {
            var authors = _context.Authors.ToList<Author>();
            var returnObj = _mapper.Map<List<AuthorViewModel>>(authors);

            return returnObj;

        }

    }

    public class AuthorViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FullName { get { return Name + " " + Surname; } set { FullName = value; } }
        public DateTime BirthDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}