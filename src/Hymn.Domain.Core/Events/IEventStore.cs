using NetDevPack.Messaging;

namespace Hymn.Domain.Core.Events
{
    public interface IEventStore
    {
        void Save<T>(T theEvent) where T : Event;
    }
}