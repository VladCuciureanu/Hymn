using FluentValidation.Results;
using Hymn.Application.Interfaces;
using Hymn.Application.Services;
using Hymn.Domain.Commands.Album;
using Hymn.Domain.Commands.Artist;
using Hymn.Domain.Commands.Song;
using Hymn.Domain.Commands.SongVersion;
using Hymn.Domain.Core.Events;
using Hymn.Domain.Events.Album;
using Hymn.Domain.Events.Artist;
using Hymn.Domain.Events.Song;
using Hymn.Domain.Events.SongVersion;
using Hymn.Domain.Interfaces;
using Hymn.Infra.CrossCutting.Bus;
using Hymn.Infra.CrossCutting.Identity.Persistence;
using Hymn.Infra.Data.EventSourcing;
using Hymn.Infra.Data.Persistence;
using Hymn.Infra.Data.Repository;
using Hymn.Infra.Data.Repository.EventSourcing;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Mediator;

namespace Hymn.Infra.CrossCutting.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // Application
            services.AddScoped<IAlbumAppService, AlbumAppService>();
            services.AddScoped<IArtistAppService, ArtistAppService>();
            services.AddScoped<ISongAppService, SongAppService>();
            services.AddScoped<ISongVersionAppService, SongVersionAppService>();

            // Domain - Events
            services.AddScoped<INotificationHandler<AlbumAddedEvent>, AlbumEventHandler>();
            services.AddScoped<INotificationHandler<AlbumUpdatedEvent>, AlbumEventHandler>();
            services.AddScoped<INotificationHandler<AlbumRemovedEvent>, AlbumEventHandler>();

            services.AddScoped<INotificationHandler<ArtistAddedEvent>, ArtistEventHandler>();
            services.AddScoped<INotificationHandler<ArtistUpdatedEvent>, ArtistEventHandler>();
            services.AddScoped<INotificationHandler<ArtistRemovedEvent>, ArtistEventHandler>();

            services.AddScoped<INotificationHandler<SongAddedEvent>, SongEventHandler>();
            services.AddScoped<INotificationHandler<SongUpdatedEvent>, SongEventHandler>();
            services.AddScoped<INotificationHandler<SongRemovedEvent>, SongEventHandler>();

            services.AddScoped<INotificationHandler<SongVersionAddedEvent>, SongVersionEventHandler>();
            services.AddScoped<INotificationHandler<SongVersionUpdatedEvent>, SongVersionEventHandler>();
            services.AddScoped<INotificationHandler<SongVersionRemovedEvent>, SongVersionEventHandler>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<CreateAlbumCommand, ValidationResult>, AlbumCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateAlbumCommand, ValidationResult>, AlbumCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveAlbumCommand, ValidationResult>, AlbumCommandHandler>();

            services.AddScoped<IRequestHandler<CreateArtistCommand, ValidationResult>, ArtistCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateArtistCommand, ValidationResult>, ArtistCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveArtistCommand, ValidationResult>, ArtistCommandHandler>();

            services.AddScoped<IRequestHandler<CreateSongCommand, ValidationResult>, SongCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateSongCommand, ValidationResult>, SongCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveSongCommand, ValidationResult>, SongCommandHandler>();

            services.AddScoped<IRequestHandler<CreateSongVersionCommand, ValidationResult>, SongVersionCommandHandler>();
            services
                .AddScoped<IRequestHandler<UpdateSongVersionCommand, ValidationResult>, SongVersionCommandHandler>();
            services
                .AddScoped<IRequestHandler<RemoveSongVersionCommand, ValidationResult>, SongVersionCommandHandler>();

            // Infra - Data
            services.AddScoped<HymnContext>();

            services.AddScoped<IAlbumRepository, AlbumRepository>();
            services.AddScoped<IArtistRepository, ArtistRepository>();
            services.AddScoped<ISongRepository, SongRepository>();
            services.AddScoped<ISongVersionRepository, SongVersionRepository>();

            // Infra - Identity
            services.AddScoped<IdentityContext>();
            services.AddScoped<IdentityContextSeed>();

            // Infra - Data EventSourcing
            services.AddScoped<IEventStoreRepository, EventStoreRepository>();
            services.AddScoped<IEventStore, EventStore>();
            services.AddScoped<EventStoreContext>();
        }
    }
}