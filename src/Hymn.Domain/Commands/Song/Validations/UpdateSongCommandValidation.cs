namespace Hymn.Domain.Commands.Song.Validations
{
    public class UpdateSongCommandValidation : SongValidation<UpdateSongCommand>
    {
        public UpdateSongCommandValidation()
        {
            ValidateId();
            ValidateName();
        }
    }
}