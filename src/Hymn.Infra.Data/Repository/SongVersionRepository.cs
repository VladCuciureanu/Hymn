using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hymn.Domain.Interfaces;
using Hymn.Domain.Models;
using Hymn.Infra.Data.Persistence;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;

namespace Hymn.Infra.Data.Repository
{
    public class SongVersionRepository : ISongVersionRepository
    {
        protected readonly HymnContext Db;
        protected readonly DbSet<SongVersion> DbSet;

        public SongVersionRepository(HymnContext context)
        {
            Db = context;
            DbSet = Db.Set<SongVersion>();
        }

        public IUnitOfWork UnitOfWork => Db;

        public async Task<SongVersion> GetById(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<IEnumerable<SongVersion>> GetByAlbumId(Guid id)
        {
            return await DbSet.Where(e => e.AlbumId == id).ToListAsync();
        }
        
        public async Task<IEnumerable<SongVersion>> GetByArtistId(Guid id)
        {
            return await DbSet.Where(e => e.ArtistId == id).ToListAsync();
        }

        public async Task<IEnumerable<SongVersion>> GetBySongId(Guid id)
        {
            return await DbSet.Where(e => e.SongId == id).ToListAsync();
        }

        public async Task<IEnumerable<SongVersion>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public void Add(SongVersion songVersion)
        {
            DbSet.Add(songVersion);
        }

        public void Update(SongVersion songVersion)
        {
            DbSet.Update(songVersion);
        }

        public void Remove(SongVersion songVersion)
        {
            DbSet.Remove(songVersion);
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}