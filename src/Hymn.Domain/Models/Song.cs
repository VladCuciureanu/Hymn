using System;
using System.Collections.Generic;
using NetDevPack.Domain;

namespace Hymn.Domain.Models
{
    public class Song : Entity, IAggregateRoot
    {
        public Song(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        // Empty constructor for EF
        public Song()
        {
        }

        public string Name { get; }

        // Relationships
        public List<SongVersion> SongVersions { get; set; }
    }
}