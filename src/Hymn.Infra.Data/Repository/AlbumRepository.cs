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
    public class AlbumRepository : IAlbumRepository
    {
        protected readonly HymnContext Db;
        protected readonly DbSet<Album> DbSet;

        public AlbumRepository(HymnContext context)
        {
            Db = context;
            DbSet = Db.Set<Album>();
        }

        public IUnitOfWork UnitOfWork => Db;

        public async Task<Album> GetById(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<IEnumerable<Album>> GetByArtistId(Guid id)
        {
            return await DbSet.Where(e => e.ArtistId == id).ToListAsync();
        }

        public async Task<IEnumerable<Album>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public void Add(Album album)
        {
            DbSet.Add(album);
        }

        public void Update(Album album)
        {
            DbSet.Update(album);
        }

        public void Remove(Album album)
        {
            DbSet.Remove(album);
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}