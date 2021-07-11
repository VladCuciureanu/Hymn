using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hymn.Domain.Models;
using NetDevPack.Data;

namespace Hymn.Domain.Interfaces
{
    public interface IArtistRepository : IRepository<Artist>
    {
        Task<Artist> GetById(Guid id);
        Task<IEnumerable<Artist>> GetAll();

        void Add(Artist artist);
        void Update(Artist artist);
        void Remove(Artist artist);
    }
}