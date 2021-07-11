using System;
using Hymn.Domain.Commands.Album.Validations;

namespace Hymn.Domain.Commands.Album
{
    public class RemoveAlbumCommand : AlbumCommand
    {
        public RemoveAlbumCommand(Guid id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveAlbumCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}