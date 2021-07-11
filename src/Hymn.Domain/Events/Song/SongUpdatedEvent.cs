using System;
using NetDevPack.Messaging;

namespace Hymn.Domain.Events.Song
{
    public class SongUpdatedEvent : Event
    {
        public SongUpdatedEvent(Guid id, string name)
        {
            Id = id;
            Name = name;
            AggregateId = id;
        }

        public Guid Id { get; }
        public string Name { get; }
    }
}