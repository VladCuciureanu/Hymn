using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hymn.Domain.Models;
using NetDevPack.Data;

namespace Hymn.Domain.Interfaces
{
    public interface ISongVersionRepository : IRepository<SongVersion>
    {
        Task<SongVersion> GetById(Guid id);
        Task<IEnumerable<SongVersion>> GetByAlbumId(Guid albumId);
        Task<IEnumerable<SongVersion>> GetByArtistId(Guid artistId);
        Task<IEnumerable<SongVersion>> GetBySongId(Guid songId);
        Task<IEnumerable<SongVersion>> GetAll();

        void Add(SongVersion songVersion);
        void Update(SongVersion songVersion);
        void Remove(SongVersion songVersion);
    }
}