using System;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {

        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateBookCommandValidatorTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Theory]
        [InlineData(1, "Test", 100, 0)]
        [InlineData(1, "Test", 0, 1)]
        [InlineData(1, "Test", 0, 0)]
        [InlineData(0, "Test", 0, 0)]

        [InlineData(1, "T", 100, 0)]
        [InlineData(0, "T", 100, 1)]
        [InlineData(0, "T", 100, 0)]
        [InlineData(0, "Test", 100, 0)]

        [InlineData(1, "T", 0, 1)]
        [InlineData(1, "T", 100, 1)]
        [InlineData(0, "T", 0, 1)]
        [InlineData(0, "Test", 0, 1)]

        [InlineData(0, "T", 0, 0)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int bookId, string title, int pageCount, int genreId)
        {
            UpdateBookCommand command = new UpdateBookCommand(null, null);
            command.BookId = bookId;
            command.Model = new UpdateBookModel
            {
                Title = title,
                PageCount = pageCount,
                GenreId = genreId,
                PublishDate = DateTime.Now.AddYears(-5)
            };
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            UpdateBookCommand command = new UpdateBookCommand(null, null);
            command.BookId = 1;
            command.Model = new UpdateBookModel
            {
                Title = "Hobbit",
                PageCount = 250,
                PublishDate = DateTime.Now.Date,
                GenreId = 1
            };

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            UpdateBookCommand command = new UpdateBookCommand(null, null);
            command.BookId = 1;
            command.Model = new UpdateBookModel
            {
                Title = "Test",
                PageCount = 100,
                PublishDate = DateTime.Now.Date.AddYears(-15),
                GenreId = 1
            };

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}