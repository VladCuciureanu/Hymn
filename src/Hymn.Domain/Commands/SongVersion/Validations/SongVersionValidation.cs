using System;
using FluentValidation;

namespace Hymn.Domain.Commands.SongVersion.Validations
{
    public abstract class SongVersionValidation<T> : AbstractValidator<T> where T : SongVersionCommand
    {
        protected void ValidateId()
        {
            RuleFor(sv => sv.Id)
                .NotEqual(Guid.Empty);
        }

        protected void ValidateAlbumId()
        {
            RuleFor(sv => sv.AlbumId)
                .NotEqual(Guid.Empty);
        }
        
        protected void ValidateArtistId()
        {
            RuleFor(sv => sv.ArtistId)
                .NotEqual(Guid.Empty);
        }

        protected void ValidateSongId()
        {
            RuleFor(sv => sv.SongId)
                .NotEqual(Guid.Empty);
        }

        protected void ValidateContent()
        {
            RuleFor(sv => sv.Content);
        }

        protected void ValidateDefaultKey()
        {
            RuleFor(sv => sv.DefaultKey)
                .NotNull().WithMessage("Please ensure you have selected the default key")
                .Must(BeValidKey).WithMessage("Please ensure you have selected a valid key");
        }

        protected void ValidateName()
        {
            RuleFor(sv => sv.Name)
                .NotEmpty().WithMessage("Please ensure you have entered the name")
                .Length(2, 100).WithMessage("The Name must have between 2 and 100 characters");
        }

        protected void ValidateViews()
        {
            RuleFor(sv => sv.Views)
                .NotNull().WithMessage("Please ensure you have entered the amount of views")
                .GreaterThanOrEqualTo(0).WithMessage("The view count must be above or equal to zero");
        }

        protected static bool BeValidKey(int key)
        {
            return 0 <= key && key <= 11;
        }
    }
}