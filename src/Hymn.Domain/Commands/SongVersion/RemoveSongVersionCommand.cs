using System;
using Hymn.Domain.Commands.SongVersion.Validations;

namespace Hymn.Domain.Commands.SongVersion
{
    public class RemoveSongVersionCommand : SongVersionCommand
    {
        public RemoveSongVersionCommand(Guid id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveSongVersionCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}