namespace Hymn.Domain.Commands.SongVersion.Validations
{
    public class RemoveSongVersionCommandValidation : SongVersionValidation<RemoveSongVersionCommand>
    {
        public RemoveSongVersionCommandValidation()
        {
            ValidateId();
        }
    }
}