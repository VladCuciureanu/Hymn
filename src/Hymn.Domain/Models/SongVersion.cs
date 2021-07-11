using System;
using NetDevPack.Domain;

namespace Hymn.Domain.Models
{
    public class SongVersion : Entity, IAggregateRoot
    {
        public SongVersion(Guid id, Guid? albumId, Guid? artistId, Guid songId, string content, int defaultKey, string name, int views)
        {
            Id = id;
            AlbumId = albumId;
            ArtistId = artistId;
            SongId = songId;
            Content = content;
            DefaultKey = defaultKey;
            Name = name;
            Views = views;
        }

        // Empty constructor for EF
        public SongVersion()
        {
        }

        public string Content { get; }
        public int DefaultKey { get; }
        public string Name { get; }
        public int Views { get; }

        // Relationships
        public Guid? AlbumId { get; }
        public Album Album { get; set; }
        public Guid? ArtistId { get; }
        public Artist Artist { get; set; }
        public Guid SongId { get; }
        public Song Song { get; set; }
    }
}