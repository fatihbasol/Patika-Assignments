using System;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {

        // [Fact]
        // public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        // {
        //     //arrange - hazırlık
        //     CreateBookCommand command = new CreateBookCommand(null, null);
        //     command.Model = new CreateBookModel
        //     {
        //         Title = "",
        //         PageCount = 0,
        //         PublishDate = DateTime.Now,
        //         GenreId = 0
        //     };
        //     //act - çalıştırma
        //     CreateBookCommandValidator validator = new CreateBookCommandValidator();
        //     var result = validator.Validate(command);

        //     //assert - doğrulama
        //     result.Errors.Count.Should().BeGreaterThan(0);
        // }

        [Theory]
        [InlineData("Lord Of The Ring", 0, 0)]
        [InlineData("Lord Of The Ring", 0, 1)]
        [InlineData("Lord Of The Ring", 100, 0)]
        [InlineData("", 0, 0)]
        [InlineData("", 100, 1)]
        [InlineData("", 0, 1)]
        [InlineData("L", 100, 1)]
        [InlineData("Lord", 100, 0)]
        [InlineData("Lord", 0, 1)]
        [InlineData(" ", 100, 1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId)
        {
            //arrange - hazırlık
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel
            {
                Title = title,
                PageCount = pageCount,
                PublishDate = DateTime.Now.AddYears(-18),
                GenreId = genreId
            };
            //act - çalıştırma
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            //assert - doğrulama
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            //arrange - hazırlık
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel
            {
                Title = "Hobbit",
                PageCount = 250,
                PublishDate = DateTime.Now.Date,
                GenreId = 1
            };

            //act - çalıştırma
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);


            //assert - doğrulama
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange - hazırlık
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel
            {
                Title = "Hobbit",
                PageCount = 250,
                PublishDate = DateTime.Now.Date.AddYears(-15),
                GenreId = 1
            };

            //act - çalıştırma
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);


            //assert - doğrulama
            result.Errors.Count.Should().Equals(0);
        }
    }
}