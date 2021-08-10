using System;
using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(command => command.AuthorId).GreaterThan(0);
            RuleFor(command => command.Model.Name).MinimumLength(2).NotEmpty();
            RuleFor(command => command.Model.Surname).MinimumLength(2).NotEmpty();
            RuleFor(command => command.Model.BirthDate.Date).LessThan(DateTime.Now.AddYears(-12).Date);
        }

    }
}