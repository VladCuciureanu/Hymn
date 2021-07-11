using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using Hymn.Domain.Events.SongVersion;
using Hymn.Domain.Extensions;
using Hymn.Domain.Interfaces;
using MediatR;
using NetDevPack.Messaging;

namespace Hymn.Domain.Commands.SongVersion
{
    public class SongVersionCommandHandler : CommandHandler,
        IRequestHandler<CreateSongVersionCommand, ValidationResult>,
        IRequestHandler<UpdateSongVersionCommand, ValidationResult>,
        IRequestHandler<RemoveSongVersionCommand, ValidationResult>
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IArtistRepository _artistRepository;
        private readonly ISongRepository _songRepository;
        private readonly ISongVersionRepository _songVersionRepository;

        public SongVersionCommandHandler(ISongVersionRepository songVersionRepository, ISongRepository songRepository,
            IArtistRepository artistRepository, IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
            _artistRepository = artistRepository;
            _songRepository = songRepository;
            _songVersionRepository = songVersionRepository;
        }

        public async Task<ValidationResult> Handle(CreateSongVersionCommand message, CancellationToken cancellationToken)
        {
            var albumCheck = IsAlbumIdValid(message);
            var artistCheck = IsArtistIdValid(message);
            var songCheck = IsSongIdValid(message);

            if (!message.IsValid() || !artistCheck.IsValid || !songCheck.IsValid || !albumCheck.IsValid)
                return albumCheck.Plus(artistCheck.Plus(songCheck).Plus(message.ValidationResult));

            var songVersion = new Models.SongVersion(Guid.NewGuid(), message.AlbumId, message.ArtistId, message.SongId, message.Content,
                message.DefaultKey, message.Name, message.Views);

            songVersion.AddDomainEvent(new SongVersionAddedEvent(songVersion.Id, message.AlbumId, songVersion.ArtistId,
                songVersion.SongId, songVersion.Content, songVersion.DefaultKey, songVersion.Name, songVersion.Views));

            _songVersionRepository.Add(songVersion);

            return await Commit(_songVersionRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(RemoveSongVersionCommand message,
            CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var songVersion = await _songVersionRepository.GetById(message.Id);

            if (songVersion is null)
            {
                var validationResult = new ValidationResult();
                validationResult.Errors.Add(new ValidationFailure("Id", "Given song version doesn't exist"));
                return validationResult;
            }

            songVersion.AddDomainEvent(new SongVersionRemovedEvent(message.Id));

            _songVersionRepository.Remove(songVersion);

            return await Commit(_songVersionRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(UpdateSongVersionCommand message,
            CancellationToken cancellationToken)
        {
            var albumCheck = IsAlbumIdValid(message);
            var artistCheck = IsArtistIdValid(message);
            var songCheck = IsSongIdValid(message);

            if (!message.IsValid() || !artistCheck.IsValid || !songCheck.IsValid || !albumCheck.IsValid)
                return albumCheck.Plus(artistCheck.Plus(songCheck).Plus(message.ValidationResult));

            var songVersion = new Models.SongVersion(message.Id, message.AlbumId, message.ArtistId, message.SongId, message.Content,
                message.DefaultKey, message.Name, message.Views);

            songVersion.AddDomainEvent(new SongVersionUpdatedEvent(songVersion.Id, message.AlbumId, songVersion.ArtistId,
                songVersion.SongId, songVersion.Content, songVersion.DefaultKey, songVersion.Name, songVersion.Views));

            _songVersionRepository.Update(songVersion);

            return await Commit(_songVersionRepository.UnitOfWork);
        }

        private ValidationResult IsAlbumIdValid(SongVersionCommand command)
        {
            var result = new ValidationResult();
            if (!command.AlbumId.HasValue)
                return result;
            if (_albumRepository.GetById(command.AlbumId.Value).Result == null)
                result.Errors.Add(new ValidationFailure("AlbumId", "Given album doesn't exist"));
            return result;
        }
        
        private ValidationResult IsArtistIdValid(SongVersionCommand command)
        {
            var result = new ValidationResult();
            if (!command.ArtistId.HasValue)
                return result;
            if (_artistRepository.GetById(command.ArtistId.Value).Result == null)
                result.Errors.Add(new ValidationFailure("ArtistId", "Given artist doesn't exist"));
            return result;
        }

        private ValidationResult IsSongIdValid(SongVersionCommand command)
        {
            var result = new ValidationResult();
            if (_songRepository.GetById(command.SongId).Result == null)
                result.Errors.Add(new ValidationFailure("SongId", "Given song doesn't exist"));
            return result;
        }

        public void Dispose()
        {
            _songVersionRepository.Dispose();
        }
    }
}