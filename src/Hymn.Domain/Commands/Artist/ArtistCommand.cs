using System;
using NetDevPack.Messaging;

namespace Hymn.Domain.Commands.Artist
{
    public class ArtistCommand : Command
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
    }
}