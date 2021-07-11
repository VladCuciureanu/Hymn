using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Hymn.Domain.Events.Album
{
    public class AlbumEventHandler :
        INotificationHandler<AlbumAddedEvent>,
        INotificationHandler<AlbumUpdatedEvent>,
        INotificationHandler<AlbumRemovedEvent>
    {
        public Task Handle(AlbumAddedEvent message, CancellationToken cancellationToken)
        {
            // TODO: Do something

            return Task.CompletedTask;
        }

        public Task Handle(AlbumRemovedEvent message, CancellationToken cancellationToken)
        {
            // TODO: Do something

            return Task.CompletedTask;
        }

        public Task Handle(AlbumUpdatedEvent message, CancellationToken cancellationToken)
        {
            // TODO: Do something

            return Task.CompletedTask;
        }
    }
}