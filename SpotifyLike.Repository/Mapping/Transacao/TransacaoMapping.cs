using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpotifyLike.Domain.Core.ValueObject;
using SpotifyLike.Domain.Streaming.Aggregates;
using SpotifyLike.Domain.Transacao.Aggregates;
using SpotifyLike.Domain.Transacao.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Repository.Mapping.Transacao
{
    public class TransacaoMapping : IEntityTypeConfiguration<Domain.Transacao.Aggregates.Transacao>
    {
        public void Configure(EntityTypeBuilder<Domain.Transacao.Aggregates.Transacao> builder)
        {
            builder.ToTable(nameof(Domain.Transacao.Aggregates.Transacao));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.DataTransacao).IsRequired();
            builder.Property(x => x.Descricao).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Autorizada).IsRequired();
            builder.OwnsOne<Merchant>(
                d => d.Recebedor, 
                c =>
                    {
                        c.Property(x => x.Nome).HasColumnName("MerchantNome").IsRequired();
                        c.Property(x => x.Email).HasColumnName("MerchantEmail");
                        c.OwnsOne<Cnpj>(
                            d => d.Cnpj, 
                            c =>
                                {
                                    c.Property(x => x.Valor).HasColumnName("MerchantCnpj");
                                });
                    });
            builder.OwnsOne<Monetario>(
                d => d.Valor, 
                c =>
                    {
                        c.Property(x => x.Valor).HasColumnName("Valor").IsRequired();
                    });
        }
    }
}
