using System;
using NetDevPack.Messaging;

namespace Hymn.Domain.Events.Artist
{
    public class ArtistAddedEvent : Event
    {
        public ArtistAddedEvent(Guid id, string name)
        {
            Id = id;
            Name = name;
            AggregateId = id;
        }

        public Guid Id { get; }
        public string Name { get; }
    }
}