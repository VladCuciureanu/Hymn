namespace Hymn.Domain.Commands.Album.Validations
{
    public class UpdateAlbumCommandValidation : AlbumValidation<UpdateAlbumCommand>
    {
        public UpdateAlbumCommandValidation()
        {
            ValidateId();
            ValidateArtistId();
            ValidateName();
        }
    }
}