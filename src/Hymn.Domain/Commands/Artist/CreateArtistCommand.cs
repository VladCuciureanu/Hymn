using Hymn.Domain.Commands.Artist.Validations;

namespace Hymn.Domain.Commands.Artist
{
    public class CreateArtistCommand : ArtistCommand
    {
        public CreateArtistCommand(string name)
        {
            Name = name;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddArtistCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}