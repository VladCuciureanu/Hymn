using System;
using Hymn.Domain.Commands.Song.Validations;

namespace Hymn.Domain.Commands.Song
{
    public class UpdateSongCommand : SongCommand
    {
        public UpdateSongCommand(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateSongCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}