using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using Hymn.Domain.Events.Artist;
using Hymn.Domain.Interfaces;
using MediatR;
using NetDevPack.Messaging;

namespace Hymn.Domain.Commands.Artist
{
    public class ArtistCommandHandler : CommandHandler,
        IRequestHandler<CreateArtistCommand, ValidationResult>,
        IRequestHandler<UpdateArtistCommand, ValidationResult>,
        IRequestHandler<RemoveArtistCommand, ValidationResult>
    {
        private readonly IArtistRepository _artistRepository;

        public ArtistCommandHandler(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public async Task<ValidationResult> Handle(CreateArtistCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var artist = new Models.Artist(Guid.NewGuid(), message.Name);

            artist.AddDomainEvent(new ArtistAddedEvent(artist.Id, artist.Name));

            _artistRepository.Add(artist);

            return await Commit(_artistRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(RemoveArtistCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var artist = await _artistRepository.GetById(message.Id);

            if (artist is null)
            {
                var validationResult = new ValidationResult();
                validationResult.Errors.Add(new ValidationFailure("Id", "Given artist doesn't exist"));
                return validationResult;
            }

            artist.AddDomainEvent(new ArtistRemovedEvent(message.Id));

            _artistRepository.Remove(artist);

            return await Commit(_artistRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(UpdateArtistCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var artist = new Models.Artist(message.Id, message.Name);

            artist.AddDomainEvent(new ArtistUpdatedEvent(artist.Id, artist.Name));

            _artistRepository.Update(artist);

            return await Commit(_artistRepository.UnitOfWork);
        }

        public void Dispose()
        {
            _artistRepository.Dispose();
        }
    }
}