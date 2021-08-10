using System;
using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(command => command.Model.BirthDate.Date).LessThan(DateTime.Now.AddYears(-12).Date);
            RuleFor(command => command.Model.Name).MinimumLength(2).NotEmpty();
            RuleFor(command => command.Model.Surname).MinimumLength(2).NotEmpty();
        }

    }
}