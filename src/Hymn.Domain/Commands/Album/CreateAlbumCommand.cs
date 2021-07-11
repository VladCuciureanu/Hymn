using System;
using Hymn.Domain.Commands.Album.Validations;

namespace Hymn.Domain.Commands.Album
{
    public class CreateAlbumCommand : AlbumCommand
    {
        public CreateAlbumCommand(Guid artistId, string name)
        {
            ArtistId = artistId;
            Name = name;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddAlbumCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}