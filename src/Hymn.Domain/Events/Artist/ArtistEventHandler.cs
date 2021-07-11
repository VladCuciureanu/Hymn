using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Hymn.Domain.Events.Artist
{
    public class ArtistEventHandler :
        INotificationHandler<ArtistAddedEvent>,
        INotificationHandler<ArtistUpdatedEvent>,
        INotificationHandler<ArtistRemovedEvent>
    {
        public Task Handle(ArtistAddedEvent message, CancellationToken cancellationToken)
        {
            // TODO: Do something

            return Task.CompletedTask;
        }

        public Task Handle(ArtistRemovedEvent message, CancellationToken cancellationToken)
        {
            // TODO: Do something

            return Task.CompletedTask;
        }

        public Task Handle(ArtistUpdatedEvent message, CancellationToken cancellationToken)
        {
            // TODO: Do something

            return Task.CompletedTask;
        }
    }
}