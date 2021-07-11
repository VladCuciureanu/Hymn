using System;
using System.Collections.Generic;
using NetDevPack.Domain;

namespace Hymn.Domain.Models
{
    public class Artist : Entity, IAggregateRoot
    {
        public Artist(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        // Empty constructor for EF
        public Artist()
        {
        }

        public string Name { get; }

        // Relationships
        public List<Album> Albums { get; set; }
    }
}