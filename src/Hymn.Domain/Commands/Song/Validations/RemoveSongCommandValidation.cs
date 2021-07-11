namespace Hymn.Domain.Commands.Song.Validations
{
    public class RemoveSongCommandValidation : SongValidation<RemoveSongCommand>
    {
        public RemoveSongCommandValidation()
        {
            ValidateId();
        }
    }
}