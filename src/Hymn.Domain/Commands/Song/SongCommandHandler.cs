using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using Hymn.Domain.Events.Song;
using Hymn.Domain.Interfaces;
using MediatR;
using NetDevPack.Messaging;

namespace Hymn.Domain.Commands.Song
{
    public class SongCommandHandler : CommandHandler,
        IRequestHandler<CreateSongCommand, ValidationResult>,
        IRequestHandler<UpdateSongCommand, ValidationResult>,
        IRequestHandler<RemoveSongCommand, ValidationResult>
    {
        private readonly ISongRepository _songRepository;

        public SongCommandHandler(ISongRepository songRepository)
        {
            _songRepository = songRepository;
        }

        public async Task<ValidationResult> Handle(CreateSongCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var song = new Models.Song(Guid.NewGuid(), message.Name);

            song.AddDomainEvent(new SongAddedEvent(song.Id, song.Name));

            _songRepository.Add(song);

            return await Commit(_songRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(RemoveSongCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var song = await _songRepository.GetById(message.Id);

            if (song is null)
            {
                var validationResult = new ValidationResult();
                validationResult.Errors.Add(new ValidationFailure("Id", "Given song doesn't exist"));
                return validationResult;
            }

            song.AddDomainEvent(new SongRemovedEvent(message.Id));

            _songRepository.Remove(song);

            return await Commit(_songRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(UpdateSongCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var song = new Models.Song(message.Id, message.Name);

            song.AddDomainEvent(new SongUpdatedEvent(song.Id, song.Name));

            _songRepository.Update(song);

            return await Commit(_songRepository.UnitOfWork);
        }

        public void Dispose()
        {
            _songRepository.Dispose();
        }
    }
}