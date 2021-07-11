using System;
using NetDevPack.Messaging;

namespace Hymn.Domain.Commands.Album
{
    public class AlbumCommand : Command
    {
        public Guid Id { get; protected set; }
        public Guid ArtistId { get; protected set; }
        public string Name { get; protected set; }
    }
}