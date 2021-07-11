using System;
using NetDevPack.Messaging;

namespace Hymn.Domain.Core.Events
{
    public class StoredEvent : Event
    {
        public StoredEvent(Event theEvent, string data, string user)
        {
            Id = Guid.NewGuid();
            AggregateId = theEvent.AggregateId;
            MessageType = theEvent.MessageType;
            Data = data;
            User = user;
        }

        // EF Constructor
        protected StoredEvent()
        {
        }

        public Guid Id { get; }

        public string Data { get; }

        public string User { get; }
    }
}