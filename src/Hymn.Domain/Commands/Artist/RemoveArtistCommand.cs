using System;
using Hymn.Domain.Commands.Artist.Validations;

namespace Hymn.Domain.Commands.Artist
{
    public class RemoveArtistCommand : ArtistCommand
    {
        public RemoveArtistCommand(Guid id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveArtistCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}