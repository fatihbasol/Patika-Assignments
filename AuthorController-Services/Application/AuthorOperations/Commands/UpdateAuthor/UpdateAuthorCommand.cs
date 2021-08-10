using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        public int AuthorId { get; set; }

        public UpdateAuthorModel Model { get; set; }
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateAuthorCommand(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);

            if (author == null)
                throw new InvalidOperationException("Güncellenecek yazar bulunamadı");

            author.Name = string.IsNullOrEmpty(Model.Name) ? author.Name : Model.Name;
            author.Surname = string.IsNullOrEmpty(Model.Surname) ? author.Surname : Model.Surname;
            author.BirthDate = Model.BirthDate != default ? Model.BirthDate : author.BirthDate;

            _context.SaveChanges();
        }
    }

    public class UpdateAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        private DateTime UpdatedDate { get; set; } = DateTime.Now;
        public DateTime BirthDate { get; set; }

    }
}