using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Hymn.Domain.Events.SongVersion
{
    public class SongVersionEventHandler :
        INotificationHandler<SongVersionAddedEvent>,
        INotificationHandler<SongVersionUpdatedEvent>,
        INotificationHandler<SongVersionRemovedEvent>
    {
        public Task Handle(SongVersionAddedEvent message, CancellationToken cancellationToken)
        {
            // TODO: Do something

            return Task.CompletedTask;
        }

        public Task Handle(SongVersionRemovedEvent message, CancellationToken cancellationToken)
        {
            // TODO: Do something

            return Task.CompletedTask;
        }

        public Task Handle(SongVersionUpdatedEvent message, CancellationToken cancellationToken)
        {
            // TODO: Do something

            return Task.CompletedTask;
        }
    }
}