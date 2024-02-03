using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpotifyLike.Repository.Migrations
{
    /// <inheritdoc />
    public partial class SpotifyDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Plano",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Valor_Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PeriodoVigenciaDias = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plano", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Album",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ArtistaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Album", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Musica",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AlbumId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Duracao_Valor = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    AlbumId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musica", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Musica_Album_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Album",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Musica_Album_AlbumId1",
                        column: x => x.AlbumId1,
                        principalTable: "Album",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Artista",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AlbumId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MusicaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artista", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artista_Album_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Album",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Artista_Musica_MusicaId",
                        column: x => x.MusicaId,
                        principalTable: "Musica",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Assinatura",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlanoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assinatura", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assinatura_Plano_PlanoId",
                        column: x => x.PlanoId,
                        principalTable: "Plano",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cartao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Limite = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProprietarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cartao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataTransacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CartaoOrigemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MerchantCnpj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MerchantNome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Recebedor_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Autorizada = table.Column<bool>(type: "bit", nullable: false),
                    CartaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transacao_Cartao_CartaoId",
                        column: x => x.CartaoId,
                        principalTable: "Cartao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transacao_Cartao_CartaoOrigemId",
                        column: x => x.CartaoOrigemId,
                        principalTable: "Cartao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MusicaPlaylist",
                columns: table => new
                {
                    MusicasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlaylistsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicaPlaylist", x => new { x.MusicasId, x.PlaylistsId });
                    table.ForeignKey(
                        name: "FK_MusicaPlaylist_Musica_MusicasId",
                        column: x => x.MusicasId,
                        principalTable: "Musica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notificacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RemetenteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DestinatarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mensagem = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Instante = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoNotificacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificacao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Playlist",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsPublica = table.Column<bool>(type: "bit", nullable: false),
                    ProprietarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlist", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SenhaHex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FavoritasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_Playlist_FavoritasId",
                        column: x => x.FavoritasId,
                        principalTable: "Playlist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Album_ArtistaId",
                table: "Album",
                column: "ArtistaId");

            migrationBuilder.CreateIndex(
                name: "IX_Artista_AlbumId",
                table: "Artista",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Artista_MusicaId",
                table: "Artista",
                column: "MusicaId");

            migrationBuilder.CreateIndex(
                name: "IX_Assinatura_PlanoId",
                table: "Assinatura",
                column: "PlanoId");

            migrationBuilder.CreateIndex(
                name: "IX_Assinatura_UsuarioId",
                table: "Assinatura",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Cartao_ProprietarioId",
                table: "Cartao",
                column: "ProprietarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Cartao_UsuarioId",
                table: "Cartao",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Musica_AlbumId",
                table: "Musica",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Musica_AlbumId1",
                table: "Musica",
                column: "AlbumId1");

            migrationBuilder.CreateIndex(
                name: "IX_MusicaPlaylist_PlaylistsId",
                table: "MusicaPlaylist",
                column: "PlaylistsId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificacao_DestinatarioId",
                table: "Notificacao",
                column: "DestinatarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificacao_RemetenteId",
                table: "Notificacao",
                column: "RemetenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Playlist_ProprietarioId",
                table: "Playlist",
                column: "ProprietarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Transacao_CartaoId",
                table: "Transacao",
                column: "CartaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Transacao_CartaoOrigemId",
                table: "Transacao",
                column: "CartaoOrigemId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_FavoritasId",
                table: "Usuario",
                column: "FavoritasId");

            migrationBuilder.AddForeignKey(
                name: "FK_Album_Artista_ArtistaId",
                table: "Album",
                column: "ArtistaId",
                principalTable: "Artista",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assinatura_Usuario_UsuarioId",
                table: "Assinatura",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cartao_Usuario_ProprietarioId",
                table: "Cartao",
                column: "ProprietarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cartao_Usuario_UsuarioId",
                table: "Cartao",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MusicaPlaylist_Playlist_PlaylistsId",
                table: "MusicaPlaylist",
                column: "PlaylistsId",
                principalTable: "Playlist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notificacao_Usuario_DestinatarioId",
                table: "Notificacao",
                column: "DestinatarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notificacao_Usuario_RemetenteId",
                table: "Notificacao",
                column: "RemetenteId",
                principalTable: "Usuario",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Playlist_Usuario_ProprietarioId",
                table: "Playlist",
                column: "ProprietarioId",
                principalTable: "Usuario",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Album_Artista_ArtistaId",
                table: "Album");

            migrationBuilder.DropForeignKey(
                name: "FK_Playlist_Usuario_ProprietarioId",
                table: "Playlist");

            migrationBuilder.DropTable(
                name: "Assinatura");

            migrationBuilder.DropTable(
                name: "MusicaPlaylist");

            migrationBuilder.DropTable(
                name: "Notificacao");

            migrationBuilder.DropTable(
                name: "Transacao");

            migrationBuilder.DropTable(
                name: "Plano");

            migrationBuilder.DropTable(
                name: "Cartao");

            migrationBuilder.DropTable(
                name: "Artista");

            migrationBuilder.DropTable(
                name: "Musica");

            migrationBuilder.DropTable(
                name: "Album");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Playlist");
        }
    }
}
