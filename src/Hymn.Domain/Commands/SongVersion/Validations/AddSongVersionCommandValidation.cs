namespace Hymn.Domain.Commands.SongVersion.Validations
{
    public class AddSongVersionCommandValidation : SongVersionValidation<CreateSongVersionCommand>
    {
        public AddSongVersionCommandValidation()
        {
            ValidateAlbumId();
            ValidateArtistId();
            ValidateSongId();
            ValidateContent();
            ValidateDefaultKey();
            ValidateName();
            ValidateViews();
        }
    }
}