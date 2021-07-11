using System;
using NetDevPack.Messaging;

namespace Hymn.Domain.Events.Song
{
    public class SongAddedEvent : Event
    {
        public SongAddedEvent(Guid id, string name)
        {
            Id = id;
            Name = name;
            AggregateId = id;
        }

        public Guid Id { get; }
        public string Name { get; }
    }
}