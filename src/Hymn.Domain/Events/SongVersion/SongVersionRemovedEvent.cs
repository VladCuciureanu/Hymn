using System;
using NetDevPack.Messaging;

namespace Hymn.Domain.Events.SongVersion
{
    public class SongVersionRemovedEvent : Event
    {
        public SongVersionRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; }
    }
}