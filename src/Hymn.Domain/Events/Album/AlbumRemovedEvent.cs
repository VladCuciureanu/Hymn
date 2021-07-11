using System;
using NetDevPack.Messaging;

namespace Hymn.Domain.Events.Album
{
    public class AlbumRemovedEvent : Event
    {
        public AlbumRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; }
    }
}