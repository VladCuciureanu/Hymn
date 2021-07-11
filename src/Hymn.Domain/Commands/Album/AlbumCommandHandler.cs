using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using Hymn.Domain.Events.Album;
using Hymn.Domain.Extensions;
using Hymn.Domain.Interfaces;
using MediatR;
using NetDevPack.Messaging;

namespace Hymn.Domain.Commands.Album
{
    public class AlbumCommandHandler : CommandHandler,
        IRequestHandler<CreateAlbumCommand, ValidationResult>,
        IRequestHandler<UpdateAlbumCommand, ValidationResult>,
        IRequestHandler<RemoveAlbumCommand, ValidationResult>
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IArtistRepository _artistRepository;

        public AlbumCommandHandler(IAlbumRepository albumRepository, IArtistRepository artistRepository)
        {
            _albumRepository = albumRepository;
            _artistRepository = artistRepository;
        }

        public async Task<ValidationResult> Handle(CreateAlbumCommand message, CancellationToken cancellationToken)
        {
            var artistCheck = IsArtistIdValid(message);

            if (!message.IsValid() || !artistCheck.IsValid)
                return artistCheck.Plus(message.ValidationResult);

            var album = new Models.Album(Guid.NewGuid(), message.ArtistId, message.Name);

            album.AddDomainEvent(new AlbumAddedEvent(album.Id, album.ArtistId, album.Name));

            _albumRepository.Add(album);

            return await Commit(_albumRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(RemoveAlbumCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var album = await _albumRepository.GetById(message.Id);

            if (album is null)
            {
                var validationResult = new ValidationResult();
                validationResult.Errors.Add(new ValidationFailure("Id", "Given album doesn't exist"));
                return validationResult;
            }

            album.AddDomainEvent(new AlbumRemovedEvent(message.Id));

            _albumRepository.Remove(album);

            return await Commit(_albumRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(UpdateAlbumCommand message, CancellationToken cancellationToken)
        {
            var artistCheck = IsArtistIdValid(message);

            if (!message.IsValid() || !artistCheck.IsValid)
                return artistCheck.Plus(message.ValidationResult);

            var album = new Models.Album(message.Id, message.ArtistId, message.Name);

            album.AddDomainEvent(new AlbumUpdatedEvent(album.Id, album.ArtistId, album.Name));

            _albumRepository.Update(album);

            return await Commit(_albumRepository.UnitOfWork);
        }

        private ValidationResult IsArtistIdValid(AlbumCommand command)
        {
            var result = new ValidationResult();
            if (_artistRepository.GetById(command.ArtistId).Result == null)
                result.Errors.Add(new ValidationFailure("ArtistId", "Given artist doesn't exist"));
            return result;
        }

        public void Dispose()
        {
            _albumRepository.Dispose();
        }
    }
}