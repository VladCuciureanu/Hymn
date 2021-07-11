using Hymn.Domain.Core.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hymn.Infra.Data.Mappings
{
    public class StoredEventMap : IEntityTypeConfiguration<StoredEvent>
    {
        public void Configure(EntityTypeBuilder<StoredEvent> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Timestamp)
                .HasColumnName("CreationDate");

            builder.Property(e => e.MessageType)
                .HasColumnName("Action")
                .HasColumnType("varchar(100)");
        }
    }
}