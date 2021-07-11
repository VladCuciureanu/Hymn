using System;
using FluentValidation;

namespace Hymn.Domain.Commands.Song.Validations
{
    public abstract class SongValidation<T> : AbstractValidator<T> where T : SongCommand
    {
        protected void ValidateId()
        {
            RuleFor(a => a.Id)
                .NotEqual(Guid.Empty);
        }

        protected void ValidateName()
        {
            RuleFor(a => a.Name)
                .NotEmpty().WithMessage("Please ensure you have entered the name")
                .Length(2, 100).WithMessage("The Name must have between 2 and 100 characters");
        }
    }
}