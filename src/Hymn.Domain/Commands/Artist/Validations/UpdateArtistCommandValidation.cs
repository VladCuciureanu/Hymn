namespace Hymn.Domain.Commands.Artist.Validations
{
    public class UpdateArtistCommandValidation : ArtistValidation<UpdateArtistCommand>
    {
        public UpdateArtistCommandValidation()
        {
            ValidateId();
            ValidateName();
        }
    }
}