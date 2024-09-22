using SpotifyLike.Domain.Core.ValueObject;
using SpotifyLike.Domain.Streaming.Aggregates;
using SpotifyLike.Domain.Transacao.Aggregates;
using SpotifyLike.Domain.Transacao.ValueObject;

namespace SpotifyLike.Domain.Conta.Aggregates
{
    public class Usuario : IIdentifier
    {
        public const string NOME_PLAYLIST_FAV = "Favoritas";
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public virtual Senha Senha { get; set; }
        public virtual IList<Cartao> Cartoes { get; set; } = new List<Cartao>();
        public virtual IList<Assinatura> Assinaturas { get; set; } = new List<Assinatura>();
        public virtual IList<Playlist> Playlists { get; set; } = null!;
        public Guid FavoritePlaylistId { get; set; }
        public virtual IList<Notificacao.Notificacao> Notificacoes { get; set; } = new List<Notificacao.Notificacao>();


        public void CriarConta(string nome, string email, string senha, DateTime dataNascimento, Cartao cartao, Plano plano)
        {
            this.Nome = nome;
            this.Email = email;
            this.DataNascimento = dataNascimento;

            this.Senha = new Senha(senha);

            this.AdicionarCartao(cartao);

            this.AtualizarAssinatura(plano, cartao);

            this.Playlists = new List<Playlist>()
            {
                new Playlist()
                {
                    Id = Guid.NewGuid(),
                    Titulo = NOME_PLAYLIST_FAV,
                    IsPublica = false,
                    DataCriacao = DateTime.Now,
                    Proprietario = this
                }
            };
            this.FavoritePlaylistId = this.Playlists[0].Id;
        }

        public void AdicionarCartao(Cartao cartao)
        {
            cartao.Proprietario = this;

            this.Cartoes.Add(cartao);
        }

        public (Assinatura, Transacao.Aggregates.Transacao) AtualizarAssinatura(Plano plano, Cartao cartao)
        {
            //Debitar o valor do plano no cartao
            var novaTransacao = cartao.RealizarTransacao(new Merchant() { Nome = plano.Tipo }, new Monetario(plano.Valor), plano.Descricao);

            //Desativo caso tenha alguma assinatura ativa
            DesativarAssinaturaAtiva();

            Assinatura novaAssinatura = new Assinatura(plano);
            novaAssinatura.Ativar();

            //Adiciona uma nova assinatura
            this.Assinaturas.Add(novaAssinatura);

            return (novaAssinatura, novaTransacao);
        }

        private void DesativarAssinaturaAtiva()
        {
            //Caso tenha alguma assintura ativa, deseativa ela
            if (this.Assinaturas.Count > 0 && this.Assinaturas.Any(x => x.Ativo))
            {
                var planoAtivo = this.Assinaturas.FirstOrDefault(x => x.Ativo);
                planoAtivo.Ativo = false;
            }
        }

        public void CriarPlaylist(string titulo, bool isPublica=true)
        {
            this.Playlists.Add(new Playlist() 
            {
                Id = Guid.NewGuid(),
                Titulo = titulo,
                IsPublica = isPublica,
                DataCriacao = DateTime.Now,
                Proprietario = this
            });
        }

        public void FavoritarMusica(Musica musica)
        {
            this.Playlists
                .Where(playlist => playlist.Id == this.FavoritePlaylistId)
                .First()
                .Adicionar(musica);
        }

        public void DesfavoritarMusica(Musica musica)
        {
            this.Playlists
                .Where(playlist => playlist.Id == this.FavoritePlaylistId)
                .First()
                .Remover(musica);
        }
    }
}
