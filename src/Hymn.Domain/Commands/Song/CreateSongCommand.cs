using Hymn.Domain.Commands.Song.Validations;

namespace Hymn.Domain.Commands.Song
{
    public class CreateSongCommand : SongCommand
    {
        public CreateSongCommand(string name)
        {
            Name = name;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddSongCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}