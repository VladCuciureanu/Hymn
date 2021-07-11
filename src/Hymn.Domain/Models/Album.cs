using System;
using NetDevPack.Domain;

namespace Hymn.Domain.Models
{
    public class Album : Entity, IAggregateRoot
    {
        public Album(Guid id, Guid artistId, string name)
        {
            Id = id;
            ArtistId = artistId;
            Name = name;
        }

        // Empty constructor for EF
        public Album()
        {
        }

        public string Name { get; }

        // Relationships
        public Guid ArtistId { get; }
        public Artist Artist { get; set; }
    }
}