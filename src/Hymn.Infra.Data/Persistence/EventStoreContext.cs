using Hymn.Domain.Core.Events;
using Hymn.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Hymn.Infra.Data.Persistence
{
    public class EventStoreContext : DbContext
    {
        public EventStoreContext(DbContextOptions<EventStoreContext> options) : base(options)
        {
        }

        public DbSet<StoredEvent> StoredEvent { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StoredEventMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}