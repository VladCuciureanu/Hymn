using System;
using NetDevPack.Messaging;

namespace Hymn.Domain.Commands.Song
{
    public class SongCommand : Command
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
    }
}