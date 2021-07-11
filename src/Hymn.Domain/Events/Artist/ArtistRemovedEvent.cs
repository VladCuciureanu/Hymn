using System;
using NetDevPack.Messaging;

namespace Hymn.Domain.Events.Artist
{
    public class ArtistRemovedEvent : Event
    {
        public ArtistRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; }
    }
}