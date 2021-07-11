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
    public class ArtistRepository : IArtistRepository
    {
        protected readonly HymnContext Db;
        protected readonly DbSet<Artist> DbSet;

        public ArtistRepository(HymnContext context)
        {
            Db = context;
            DbSet = Db.Set<Artist>();
        }

        public IUnitOfWork UnitOfWork => Db;

        public async Task<Artist> GetById(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<IEnumerable<Artist>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public void Add(Artist artist)
        {
            DbSet.Add(artist);
        }

        public void Update(Artist artist)
        {
            DbSet.Update(artist);
        }

        public void Remove(Artist artist)
        {
            DbSet.Remove(artist);
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}