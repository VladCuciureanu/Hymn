using Hymn.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hymn.Infra.Data.Mappings
{
    public class AlbumMap : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.Property(e => e.Id)
                .HasColumnName("Id");

            builder.Property(e => e.ArtistId);

            builder.Property(e => e.Name)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Artist)
                .WithMany(e => e.Albums)
                .HasForeignKey(e => e.ArtistId);
        }
    }
}