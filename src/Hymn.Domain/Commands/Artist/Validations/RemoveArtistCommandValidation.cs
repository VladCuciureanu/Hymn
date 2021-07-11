namespace Hymn.Domain.Commands.Artist.Validations
{
    public class RemoveArtistCommandValidation : ArtistValidation<RemoveArtistCommand>
    {
        public RemoveArtistCommandValidation()
        {
            ValidateId();
        }
    }
}