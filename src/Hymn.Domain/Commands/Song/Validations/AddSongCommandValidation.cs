namespace Hymn.Domain.Commands.Song.Validations
{
    public class AddSongCommandValidation : SongValidation<CreateSongCommand>
    {
        public AddSongCommandValidation()
        {
            ValidateName();
        }
    }
}