namespace Hymn.Domain.Commands.Album.Validations
{
    public class AddAlbumCommandValidation : AlbumValidation<CreateAlbumCommand>
    {
        public AddAlbumCommandValidation()
        {
            ValidateArtistId();
            ValidateName();
        }
    }
}