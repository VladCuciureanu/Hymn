using System;
using NetDevPack.Messaging;

namespace Hymn.Domain.Events.Album
{
    public class AlbumUpdatedEvent : Event
    {
        public AlbumUpdatedEvent(Guid id, Guid artistId, string name)
        {
            Id = id;
            ArtistId = artistId;
            Name = name;
            AggregateId = id;
        }

        public Guid Id { get; }
        public Guid ArtistId { get; }
        public string Name { get; }
    }
}