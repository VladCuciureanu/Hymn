using Hymn.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hymn.Infra.Data.Mappings
{
    public class SongVersionMap : IEntityTypeConfiguration<SongVersion>
    {
        public void Configure(EntityTypeBuilder<SongVersion> builder)
        {
            builder.Property(e => e.Id)
                .HasColumnName("Id");

            builder.Property(e => e.AlbumId);
            
            builder.Property(e => e.ArtistId);

            builder.Property(e => e.SongId);

            builder.Property(e => e.Content)
                .IsRequired();

            builder.Property(e => e.DefaultKey)
                .IsRequired();

            builder.Property(e => e.Name)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.Views)
                .IsRequired();

            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Song)
                .WithMany(e => e.SongVersions)
                .HasForeignKey(e => e.SongId);
        }
    }
}