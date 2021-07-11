using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Hymn.Domain.Events.Song
{
    public class SongEventHandler :
        INotificationHandler<SongAddedEvent>,
        INotificationHandler<SongUpdatedEvent>,
        INotificationHandler<SongRemovedEvent>
    {
        public Task Handle(SongAddedEvent message, CancellationToken cancellationToken)
        {
            // TODO: Do something

            return Task.CompletedTask;
        }

        public Task Handle(SongRemovedEvent message, CancellationToken cancellationToken)
        {
            // TODO: Do something

            return Task.CompletedTask;
        }

        public Task Handle(SongUpdatedEvent message, CancellationToken cancellationToken)
        {
            // TODO: Do something

            return Task.CompletedTask;
        }
    }
}