﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SpotifyLike.Repository;

#nullable disable

namespace SpotifyLike.Repository.Migrations
{
    [DbContext(typeof(SpotifyLikeContext))]
    partial class SpotifyLikeContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AlbumArtista", b =>
                {
                    b.Property<Guid>("AlbumsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ArtistasId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AlbumsId", "ArtistasId");

                    b.HasIndex("ArtistasId");

                    b.ToTable("AlbumArtista");
                });

            modelBuilder.Entity("ArtistaMusica", b =>
                {
                    b.Property<Guid>("ArtistasId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MusicasId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ArtistasId", "MusicasId");

                    b.HasIndex("MusicasId");

                    b.ToTable("ArtistaMusica");
                });

            modelBuilder.Entity("MusicaPlaylist", b =>
                {
                    b.Property<Guid>("MusicasId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PlaylistId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("MusicasId", "PlaylistId");

                    b.HasIndex("PlaylistId");

                    b.ToTable("MusicaPlaylist");
                });

            modelBuilder.Entity("SpotifyLike.Domain.Conta.Aggregates.Assinatura", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataVencimento")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("PlanoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UsuarioId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PlanoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Assinatura", (string)null);
                });

            modelBuilder.Entity("SpotifyLike.Domain.Conta.Aggregates.Playlist", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsPublica")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ProprietarioId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("ProprietarioId");

                    b.ToTable("Playlist", (string)null);
                });

            modelBuilder.Entity("SpotifyLike.Domain.Conta.Aggregates.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<Guid>("FavoritasId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("FavoritasId");

                    b.ToTable("Usuario", (string)null);
                });

            modelBuilder.Entity("SpotifyLike.Domain.Notificacao.Notificacao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DestinatarioId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Instante")
                        .HasColumnType("datetime2");

                    b.Property<string>("Mensagem")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<Guid>("RemetenteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("TipoNotificacao")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.HasIndex("DestinatarioId");

                    b.HasIndex("RemetenteId");

                    b.ToTable("Notificacao", (string)null);
                });

            modelBuilder.Entity("SpotifyLike.Domain.Streaming.Aggregates.Album", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Album", (string)null);
                });

            modelBuilder.Entity("SpotifyLike.Domain.Streaming.Aggregates.Artista", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Backdrop")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Artista", (string)null);
                });

            modelBuilder.Entity("SpotifyLike.Domain.Streaming.Aggregates.Musica", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AlbumId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.ToTable("Musica", (string)null);
                });

            modelBuilder.Entity("SpotifyLike.Domain.Streaming.Aggregates.Plano", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Plano", (string)null);
                });

            modelBuilder.Entity("SpotifyLike.Domain.Transacao.Aggregates.Cartao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DataVencimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("ProprietarioId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProprietarioId");

                    b.ToTable("Cartao", (string)null);
                });

            modelBuilder.Entity("SpotifyLike.Domain.Transacao.Aggregates.Transacao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Autorizada")
                        .HasColumnType("bit");

                    b.Property<Guid>("CartaoOrigemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataTransacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("CartaoOrigemId");

                    b.ToTable("Transacao", (string)null);
                });

            modelBuilder.Entity("AlbumArtista", b =>
                {
                    b.HasOne("SpotifyLike.Domain.Streaming.Aggregates.Album", null)
                        .WithMany()
                        .HasForeignKey("AlbumsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SpotifyLike.Domain.Streaming.Aggregates.Artista", null)
                        .WithMany()
                        .HasForeignKey("ArtistasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ArtistaMusica", b =>
                {
                    b.HasOne("SpotifyLike.Domain.Streaming.Aggregates.Artista", null)
                        .WithMany()
                        .HasForeignKey("ArtistasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SpotifyLike.Domain.Streaming.Aggregates.Musica", null)
                        .WithMany()
                        .HasForeignKey("MusicasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MusicaPlaylist", b =>
                {
                    b.HasOne("SpotifyLike.Domain.Streaming.Aggregates.Musica", null)
                        .WithMany()
                        .HasForeignKey("MusicasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SpotifyLike.Domain.Conta.Aggregates.Playlist", null)
                        .WithMany()
                        .HasForeignKey("PlaylistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SpotifyLike.Domain.Conta.Aggregates.Assinatura", b =>
                {
                    b.HasOne("SpotifyLike.Domain.Streaming.Aggregates.Plano", "Plano")
                        .WithMany()
                        .HasForeignKey("PlanoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SpotifyLike.Domain.Conta.Aggregates.Usuario", null)
                        .WithMany("Assinaturas")
                        .HasForeignKey("UsuarioId");

                    b.Navigation("Plano");
                });

            modelBuilder.Entity("SpotifyLike.Domain.Conta.Aggregates.Playlist", b =>
                {
                    b.HasOne("SpotifyLike.Domain.Conta.Aggregates.Usuario", "Proprietario")
                        .WithMany("Playlists")
                        .HasForeignKey("ProprietarioId");

                    b.Navigation("Proprietario");
                });

            modelBuilder.Entity("SpotifyLike.Domain.Conta.Aggregates.Usuario", b =>
                {
                    b.HasOne("SpotifyLike.Domain.Conta.Aggregates.Playlist", "Favoritas")
                        .WithMany()
                        .HasForeignKey("FavoritasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("SpotifyLike.Domain.Core.ValueObject.Senha", "Senha", b1 =>
                        {
                            b1.Property<Guid>("UsuarioId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("HexValue")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("SenhaHex");

                            b1.HasKey("UsuarioId");

                            b1.ToTable("Usuario");

                            b1.WithOwner()
                                .HasForeignKey("UsuarioId");
                        });

                    b.Navigation("Favoritas");

                    b.Navigation("Senha")
                        .IsRequired();
                });

            modelBuilder.Entity("SpotifyLike.Domain.Notificacao.Notificacao", b =>
                {
                    b.HasOne("SpotifyLike.Domain.Conta.Aggregates.Usuario", "Destinatario")
                        .WithMany("Notificacoes")
                        .HasForeignKey("DestinatarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SpotifyLike.Domain.Conta.Aggregates.Usuario", "Remetente")
                        .WithMany()
                        .HasForeignKey("RemetenteId");

                    b.Navigation("Destinatario");

                    b.Navigation("Remetente");
                });

            modelBuilder.Entity("SpotifyLike.Domain.Streaming.Aggregates.Musica", b =>
                {
                    b.HasOne("SpotifyLike.Domain.Streaming.Aggregates.Album", "Album")
                        .WithMany("Musicas")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("SpotifyLike.Domain.Streaming.ValueObject.Duracao", "Duracao", b1 =>
                        {
                            b1.Property<Guid>("MusicaId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Valor")
                                .HasMaxLength(50)
                                .HasColumnType("int")
                                .HasColumnName("Duracao");

                            b1.HasKey("MusicaId");

                            b1.ToTable("Musica");

                            b1.WithOwner()
                                .HasForeignKey("MusicaId");
                        });

                    b.Navigation("Album");

                    b.Navigation("Duracao")
                        .IsRequired();
                });

            modelBuilder.Entity("SpotifyLike.Domain.Streaming.Aggregates.Plano", b =>
                {
                    b.OwnsOne("SpotifyLike.Domain.Core.ValueObject.Monetario", "Valor", b1 =>
                        {
                            b1.Property<Guid>("PlanoId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Valor")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("Valor");

                            b1.HasKey("PlanoId");

                            b1.ToTable("Plano");

                            b1.WithOwner()
                                .HasForeignKey("PlanoId");
                        });

                    b.OwnsOne("SpotifyLike.Domain.Streaming.ValueObject.Periodo", "Vigencia", b1 =>
                        {
                            b1.Property<Guid>("PlanoId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("NumDias")
                                .HasColumnType("int")
                                .HasColumnName("PeriodoVigenciaDias");

                            b1.HasKey("PlanoId");

                            b1.ToTable("Plano");

                            b1.WithOwner()
                                .HasForeignKey("PlanoId");
                        });

                    b.Navigation("Valor")
                        .IsRequired();

                    b.Navigation("Vigencia")
                        .IsRequired();
                });

            modelBuilder.Entity("SpotifyLike.Domain.Transacao.Aggregates.Cartao", b =>
                {
                    b.HasOne("SpotifyLike.Domain.Conta.Aggregates.Usuario", "Proprietario")
                        .WithMany("Cartoes")
                        .HasForeignKey("ProprietarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("SpotifyLike.Domain.Core.ValueObject.Monetario", "Limite", b1 =>
                        {
                            b1.Property<Guid>("CartaoId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Valor")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("Limite");

                            b1.HasKey("CartaoId");

                            b1.ToTable("Cartao");

                            b1.WithOwner()
                                .HasForeignKey("CartaoId");
                        });

                    b.Navigation("Limite")
                        .IsRequired();

                    b.Navigation("Proprietario");
                });

            modelBuilder.Entity("SpotifyLike.Domain.Transacao.Aggregates.Transacao", b =>
                {
                    b.HasOne("SpotifyLike.Domain.Transacao.Aggregates.Cartao", "CartaoOrigem")
                        .WithMany("Transacoes")
                        .HasForeignKey("CartaoOrigemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("SpotifyLike.Domain.Core.ValueObject.Monetario", "Valor", b1 =>
                        {
                            b1.Property<Guid>("TransacaoId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Valor")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("Valor");

                            b1.HasKey("TransacaoId");

                            b1.ToTable("Transacao");

                            b1.WithOwner()
                                .HasForeignKey("TransacaoId");
                        });

                    b.OwnsOne("SpotifyLike.Domain.Transacao.ValueObject.Merchant", "Recebedor", b1 =>
                        {
                            b1.Property<Guid>("TransacaoId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Email")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("MerchantEmail");

                            b1.Property<string>("Nome")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("MerchantNome");

                            b1.HasKey("TransacaoId");

                            b1.ToTable("Transacao");

                            b1.WithOwner()
                                .HasForeignKey("TransacaoId");

                            b1.OwnsOne("SpotifyLike.Domain.Core.ValueObject.Cnpj", "Cnpj", b2 =>
                                {
                                    b2.Property<Guid>("MerchantTransacaoId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("Valor")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)")
                                        .HasColumnName("MerchantCnpj");

                                    b2.HasKey("MerchantTransacaoId");

                                    b2.ToTable("Transacao");

                                    b2.WithOwner()
                                        .HasForeignKey("MerchantTransacaoId");
                                });

                            b1.Navigation("Cnpj")
                                .IsRequired();
                        });

                    b.Navigation("CartaoOrigem");

                    b.Navigation("Recebedor")
                        .IsRequired();

                    b.Navigation("Valor")
                        .IsRequired();
                });

            modelBuilder.Entity("SpotifyLike.Domain.Conta.Aggregates.Usuario", b =>
                {
                    b.Navigation("Assinaturas");

                    b.Navigation("Cartoes");

                    b.Navigation("Notificacoes");

                    b.Navigation("Playlists");
                });

            modelBuilder.Entity("SpotifyLike.Domain.Streaming.Aggregates.Album", b =>
                {
                    b.Navigation("Musicas");
                });

            modelBuilder.Entity("SpotifyLike.Domain.Transacao.Aggregates.Cartao", b =>
                {
                    b.Navigation("Transacoes");
                });
#pragma warning restore 612, 618
        }
    }
}
