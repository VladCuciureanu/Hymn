using System;
using NetDevPack.Messaging;

namespace Hymn.Domain.Events.SongVersion
{
    public class SongVersionUpdatedEvent : Event
    {
        public SongVersionUpdatedEvent(Guid id, Guid? albumId, Guid? artistId, Guid songId, string content, int defaultKey, string name,
            int views)
        {
            Id = id;
            AlbumId = albumId;
            ArtistId = artistId;
            SongId = songId;
            Content = content;
            DefaultKey = defaultKey;
            Name = name;
            Views = views;
            AggregateId = id;
        }

        public Guid Id { get; }
        public Guid? AlbumId { get; }
        public Guid? ArtistId { get; }
        public Guid SongId { get; }
        public string Content { get; }
        public int DefaultKey { get; }
        public string Name { get; }
        public int Views { get; }
    }
}