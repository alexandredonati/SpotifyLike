using SpotifyLike.Domain.Core.ValueObject;
using SpotifyLike.Domain.Streaming.Aggregates;
using SpotifyLike.Domain.Transacao.Aggregates;
using SpotifyLike.Domain.Transacao.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Domain.Conta.Aggregates
{
    public class Usuario
    {
        public const string NOME_PLAYLIST_FAV = "Favoritas";
        public Guid Id { get; set; }
        public String Nome { get; set; }
        public String Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public Senha Senha { get; set; }
        public List<Cartao> Cartoes { get; set; } = new List<Cartao>();
        public List<Assinatura> Assinaturas { get; set; } = new List<Assinatura>();
        public List<Playlist> Playlists { get; set; } = new List<Playlist>();
        public Playlist Favoritas { get; set; }
        public List<Notificacao.Notificacao> Notificacoes { get; set; } = new List<Notificacao.Notificacao>();


        public void CriarConta(string nome, string email, string senha, DateTime dataNascimento, Cartao cartao, Plano plano) 
        {
            this.Nome = nome;
            this.Email = email;
            this.DataNascimento = dataNascimento;

            this.Senha = new Core.ValueObject.Senha(senha);

            this.AdicionarCartao(cartao);

            this.AtualizarAssinatura(plano, cartao);

            this.Favoritas = new Playlist()
            {
                Id = Guid.NewGuid(),
                Titulo = NOME_PLAYLIST_FAV,
                IsPublica = false,
                DataCriacao = DateTime.Now,
                Proprietario = this
            };
        }

        public void AdicionarCartao(Cartao cartao)
        {
            cartao.Proprietario = this;

            this.Cartoes.Add(cartao);
        }

        public void AtualizarAssinatura(Plano plano, Cartao cartao)
        {
            //Debitar o valor do plano no cartao
            cartao.RealizarTransacao(new Merchant() { Nome = plano.Tipo }, new Monetario(plano.Valor), plano.Descricao);

            //Desativo caso tenha alguma assinatura ativa
            DesativarAssinaturaAtiva();

            Assinatura NovaAssinatura = new Assinatura(plano);
            NovaAssinatura.Ativar();

            //Adiciona uma nova assinatura
            this.Assinaturas.Add(NovaAssinatura);

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
            this.Favoritas.Adicionar(musica);
        }

        public void DesfavoritarMusica(Musica musica)
        {
            this.Favoritas.Remover(musica);
        }
    }
}
