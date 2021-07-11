using System;
using Hymn.Domain.Commands.Album.Validations;

namespace Hymn.Domain.Commands.Album
{
    public class UpdateAlbumCommand : AlbumCommand
    {
        public UpdateAlbumCommand(Guid id, Guid artistId, string name)
        {
            Id = id;
            ArtistId = artistId;
            Name = name;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateAlbumCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}