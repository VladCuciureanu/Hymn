using System;
using NetDevPack.Messaging;

namespace Hymn.Domain.Commands.SongVersion
{
    public class SongVersionCommand : Command
    {
        public Guid Id { get; protected set; }
        public Guid? AlbumId { get; protected set; }
        public Guid? ArtistId { get; protected set; }
        public Guid SongId { get; protected set; }
        public string Content { get; protected set; }
        public int DefaultKey { get; protected set; }
        public string Name { get; protected set; }
        public int Views { get; protected set; }
    }
}