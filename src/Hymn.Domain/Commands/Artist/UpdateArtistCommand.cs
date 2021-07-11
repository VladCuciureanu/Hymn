using System;
using Hymn.Domain.Commands.Artist.Validations;

namespace Hymn.Domain.Commands.Artist
{
    public class UpdateArtistCommand : ArtistCommand
    {
        public UpdateArtistCommand(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateArtistCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}