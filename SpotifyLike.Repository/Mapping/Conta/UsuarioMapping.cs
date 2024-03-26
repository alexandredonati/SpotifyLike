using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpotifyLike.Domain.Conta.Aggregates;
using SpotifyLike.Domain.Core.ValueObject;
using SpotifyLike.Domain.Streaming.Aggregates;
using SpotifyLike.Domain.Streaming.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Repository.Mapping.Conta
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable(nameof(Usuario));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(150);
            builder.Property(x => x.DataNascimento).IsRequired();
            builder.HasMany(x => x.Cartoes).WithOne(x => x.Proprietario);
            builder.HasMany(x => x.Assinaturas).WithOne();
            builder.HasMany(x => x.Playlists).WithOne(x => x.Proprietario);
            builder.Property(x => x.FavoritePlaylistId).IsRequired();
            builder.HasMany(x => x.Notificacoes).WithOne(x => x.Destinatario);
            builder.OwnsOne<Senha>(
                d => d.Senha,
                c =>
                    {
                        c.Property(x => x.HexValue).HasColumnName("SenhaHex").IsRequired();
                    });
        }
    }
}
