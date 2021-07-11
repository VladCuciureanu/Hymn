namespace Hymn.Domain.Commands.Album.Validations
{
    public class RemoveAlbumCommandValidation : AlbumValidation<RemoveAlbumCommand>
    {
        public RemoveAlbumCommandValidation()
        {
            ValidateId();
        }
    }
}