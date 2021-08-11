using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {

        [Fact]
        public void WhenInvalidIdIsGiven_Validator_ShouldBeReturnErrors()
        {
            //arrange
            int id = 0; //valid ids = 1 - 2 - 3

            DeleteBookCommand command = new DeleteBookCommand(null, id);
            command.BookId = id;
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);

        }

    }
}