using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public DeleteBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenInvalidIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            int id = 5555;

            //act
            DeleteBookCommand command = new DeleteBookCommand(_context, id);
            command.BookId = id;

            //assert
            FluentActions.Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinecek Kitap bulunamadı");

        }
        [Fact]
        public void WhenValidIdIsGiven_Book_ShouldBeDeleted()
        {
            //arrange
            int id = 1;

            //act
            DeleteBookCommand command = new DeleteBookCommand(_context, id);
            command.BookId = id;

            //assert
            FluentActions.Invoking(() => command.Handle())
            .Should().NotThrow();

        }

    }
}