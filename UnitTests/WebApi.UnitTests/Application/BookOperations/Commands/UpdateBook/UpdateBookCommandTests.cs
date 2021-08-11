using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenInvalidIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            var invalidId = 0;
            UpdateBookCommand command = new UpdateBookCommand(_context,_mapper);
            command.BookId = invalidId;

            FluentActions.Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>("Güncelleme yapılacak kitap bulunamadı");

        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeUpdated()
        {
            var id = 1;
            UpdateBookCommand command = new UpdateBookCommand(_context,_mapper);
            command.BookId = id;
            command.Model = new UpdateBookModel
            {
                GenreId = 1,
                PageCount = 100,
                Title = "Test",
                PublishDate = DateTime.Now.Date.AddYears(-1)
            };
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var book = _context.Books.SingleOrDefault(x => x.Id == id);
            book.GenreId.Should().Be(command.Model.GenreId);
            book.PageCount.Should().Be(command.Model.PageCount);
            book.Title.Should().Be(command.Model.Title);
            book.PublishDate.Should().Be(command.Model.PublishDate);
            result.Errors.Count.Should().Be(0); // to be sure of date is valid

        }
    }
}