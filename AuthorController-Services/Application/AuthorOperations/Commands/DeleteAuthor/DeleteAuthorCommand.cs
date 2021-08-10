using System;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        public int AuthorId { get; set; }
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public DeleteAuthorCommand(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var author = _context.Authors.Include(x => x.Books).SingleOrDefault(x => x.Id == AuthorId);

            //Console.WriteLine(JsonConvert.SerializeObject(author.Books, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            if(author == null)
                throw new InvalidOperationException("Silinecek yazar bulunamadı.");
            if (author.Books.Count > 0)
                throw new InvalidOperationException("Kitabı yayında olan bir yazarı silemezsiniz. Önce kitabı silmelisin.");


            _context.Authors.Remove(author);
            _context.SaveChanges();

        }

    }
}