using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hymn.Domain.Models;
using NetDevPack.Data;

namespace Hymn.Domain.Interfaces
{
    public interface ISongRepository : IRepository<Song>
    {
        Task<Song> GetById(Guid id);
        Task<IEnumerable<Song>> GetAll();

        void Add(Song song);
        void Update(Song song);
        void Remove(Song song);
    }
}