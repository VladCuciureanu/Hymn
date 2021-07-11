using System;
using Hymn.Domain.Commands.SongVersion.Validations;

namespace Hymn.Domain.Commands.SongVersion
{
    public class CreateSongVersionCommand : SongVersionCommand
    {
        public CreateSongVersionCommand(Guid? albumId, Guid? artistId, Guid songId, string content, int defaultKey, string name, int views)
        {
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
            ValidationResult = new AddSongVersionCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}