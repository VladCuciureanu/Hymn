namespace Hymn.Domain.Commands.SongVersion.Validations
{
    public class UpdateSongVersionCommandValidation : SongVersionValidation<UpdateSongVersionCommand>
    {
        public UpdateSongVersionCommandValidation()
        {
            ValidateId();
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