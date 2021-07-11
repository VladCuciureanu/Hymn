using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hymn.Domain.Models;
using NetDevPack.Data;

namespace Hymn.Domain.Interfaces
{
    public interface IAlbumRepository : IRepository<Album>
    {
        Task<Album> GetById(Guid id);
        Task<IEnumerable<Album>> GetByArtistId(Guid artistId);
        Task<IEnumerable<Album>> GetAll();

        void Add(Album album);
        void Update(Album album);
        void Remove(Album album);
    }
}