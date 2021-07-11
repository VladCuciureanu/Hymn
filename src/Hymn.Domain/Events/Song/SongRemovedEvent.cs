using System;
using NetDevPack.Messaging;

namespace Hymn.Domain.Events.Song
{
    public class SongRemovedEvent : Event
    {
        public SongRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; }
    }
}