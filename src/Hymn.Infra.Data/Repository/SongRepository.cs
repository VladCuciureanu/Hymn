using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hymn.Domain.Interfaces;
using Hymn.Domain.Models;
using Hymn.Infra.Data.Persistence;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;

namespace Hymn.Infra.Data.Repository
{
    public class SongRepository : ISongRepository
    {
        protected readonly HymnContext Db;
        protected readonly DbSet<Song> DbSet;

        public SongRepository(HymnContext context)
        {
            Db = context;
            DbSet = Db.Set<Song>();
        }

        public IUnitOfWork UnitOfWork => Db;

        public async Task<Song> GetById(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<IEnumerable<Song>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public void Add(Song song)
        {
            DbSet.Add(song);
        }

        public void Update(Song song)
        {
            DbSet.Update(song);
        }

        public void Remove(Song song)
        {
            DbSet.Remove(song);
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}