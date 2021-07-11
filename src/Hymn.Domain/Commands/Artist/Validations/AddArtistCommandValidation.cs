namespace Hymn.Domain.Commands.Artist.Validations
{
    public class AddArtistCommandValidation : ArtistValidation<CreateArtistCommand>
    {
        public AddArtistCommandValidation()
        {
            ValidateName();
        }
    }
}