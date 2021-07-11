using System;
using Hymn.Domain.Commands.SongVersion.Validations;

namespace Hymn.Domain.Commands.SongVersion
{
    public class UpdateSongVersionCommand : SongVersionCommand
    {
        public UpdateSongVersionCommand(Guid id, Guid? albumId, Guid? artistId, Guid songId, string content, int defaultKey,
            string name, int views)
        {
            Id = id;
            AlbumId = albumId;
            ArtistId = artistId;
            SongId = songId;
            Content = content;
            DefaultKey = defaultKey;
            Name = name;
            Views = views;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateSongVersionCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}