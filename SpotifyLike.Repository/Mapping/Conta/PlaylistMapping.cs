using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpotifyLike.Domain.Conta.Aggregates;
using SpotifyLike.Domain.Streaming.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Repository.Mapping.Conta
{
    public class PlaylistMapping : IEntityTypeConfiguration<Playlist>

    {
        public void Configure(EntityTypeBuilder<Playlist> builder)
        {
            builder.ToTable(nameof(Playlist));

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Titulo).IsRequired().HasMaxLength(50);
            builder.Property(x => x.IsPublica).IsRequired();
            builder.Property(x => x.DataCriacao).IsRequired();

            builder.HasMany(x => x.Musicas);

        }
    }
}
