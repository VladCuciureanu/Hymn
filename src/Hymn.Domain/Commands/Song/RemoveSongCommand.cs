using System;
using Hymn.Domain.Commands.Song.Validations;

namespace Hymn.Domain.Commands.Song
{
    public class RemoveSongCommand : SongCommand
    {
        public RemoveSongCommand(Guid id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveSongCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}