using SpotifyLike.Domain.Core.ValueObject;
using SpotifyLike.Domain.Conta.Aggregates;
using SpotifyLike.Domain.Streaming.Aggregates;
using SpotifyLike.Domain.Streaming.ValueObject;
using SpotifyLike.Domain.Transacao.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Tests.Domain
{
    public class UsuarioTests
    {

        [Fact]
        public void DeveCriarUsuarioComSucesso()
        {
            Plano plano = new Plano()
            {
                Id = Guid.NewGuid(),
                Tipo = "Plano Dummy",
                Valor = 19.90M,
                Vigencia = new Periodo(30),
                Descricao = "Lorem ipsum"
            };

            Cartao cartao = new Cartao()
            {
                Id = Guid.NewGuid(),
                Numero = "123123123",
                Ativo = true,
                DataVencimento = DateTime.Today.AddYears(1),
                Limite = 100M
            };

            string nome = "Dummy Usuario";
            string email = "teste@teste.com";
            string senha = "123456";
            DateTime dataNascimento = new DateTime(1990, 3, 9);

            //Act
            Usuario usuario = new Usuario();
            usuario.CriarConta(nome, email, senha, dataNascimento, cartao, plano);

            //Assert
            Assert.NotNull(usuario.Email);
            Assert.NotNull(usuario.Nome);
            Assert.True(usuario.Email == email);
            Assert.True(usuario.Nome == nome);
            Assert.True(usuario.Senha.HexValue == new Senha(senha).HexValue);

            Assert.True(usuario.Assinaturas.Count > 0);
            Assert.Same(usuario.Assinaturas[0].Plano, plano);

            Assert.True(usuario.Cartoes.Count > 0);
            Assert.Same(usuario.Cartoes[0], cartao);

            Assert.True(usuario.Favoritas.Titulo == Usuario.NOME_PLAYLIST_FAV);
            Assert.False(usuario.Favoritas.IsPublica);
        }

        [Fact]
        public void NaoDeveCriarUsuarioComCartaoSemLimite()
        {
            Plano plano = new Plano()
            {
                Id = Guid.NewGuid(),
                Tipo = "Plano Dummy",
                Valor = 19.90M,
                Vigencia = new Periodo(30),
                Descricao = "Lorem ipsum"
            };

            Cartao cartao = new Cartao()
            {
                Id = Guid.NewGuid(),
                Numero = "123123123",
                Ativo = true,
                DataVencimento = DateTime.Today.AddYears(1),
                Limite = 10M
            };

            string nome = "Dummy Usuario";
            string email = "teste@teste.com";
            string senha = "123456";
            DateTime dataNascimento = new DateTime(1990, 3, 9);

            //Act
            Assert.Throws<Exception>(() =>
            {
                Usuario usuario = new Usuario();
                usuario.CriarConta(nome, email, senha, dataNascimento, cartao, plano);
            });
        }

        [Fact]
        public void NaoDeveCriarUsuarioComCartaoVencido()
        {
            Plano plano = new Plano()
            {
                Id = Guid.NewGuid(),
                Tipo = "Plano Dummy",
                Valor = 19.90M,
                Vigencia = new Periodo(30),
                Descricao = "Lorem ipsum"
            };

            Cartao cartao = new Cartao()
            {
                Id = Guid.NewGuid(),
                Numero = "123123123",
                Ativo = true,
                DataVencimento = DateTime.Today.AddDays(-1),
                Limite = 100M
            };

            string nome = "Dummy Usuario";
            string email = "teste@teste.com";
            string senha = "123456";
            DateTime dataNascimento = new DateTime(1990, 3, 9);

            //Act
            Assert.Throws<Exception>(() =>
            {
                Usuario usuario = new Usuario();
                usuario.CriarConta(nome, email, senha, dataNascimento, cartao, plano);
            });
        }

        [Fact]
        public void DeveManterApenasUmaAssinaturaAtiva()
        {
            Plano plano = new Plano()
            {
                Id = Guid.NewGuid(),
                Tipo = "Plano Dummy",
                Valor = 19.90M,
                Vigencia = new Periodo(30),
                Descricao = "Lorem ipsum"
            };

            Cartao cartao = new Cartao()
            {
                Id = Guid.NewGuid(),
                Numero = "123123123",
                Ativo = true,
                DataVencimento = DateTime.Today.AddYears(1),
                Limite = 100M
            };

            string nome = "Dummy Usuario";
            string email = "teste@teste.com";
            string senha = "123456";
            DateTime dataNascimento = new DateTime(1990, 3, 9);

            //Act
            Usuario usuario = new Usuario();
            usuario.CriarConta(nome, email, senha, dataNascimento, cartao, plano);

            Plano novoPlano = new Plano()
            {
                Id = Guid.NewGuid(),
                Tipo = "Plano Dummy 2",
                Valor = 39.90M,
                Vigencia = new Periodo(90),
                Descricao = "Novo plano com maior vigência"
            };

            Assert.True(usuario.Assinaturas.Count(x => x.Ativo) == 1);
            Assert.True(usuario.Assinaturas.Where(x => x.Ativo).First().Plano.Id == plano.Id);

            usuario.AtualizarAssinatura(novoPlano, cartao);

            Assert.True(usuario.Assinaturas.Count(x => x.Ativo) == 1);
            Assert.True(usuario.Assinaturas.Where(x => x.Ativo).First().Plano.Id == novoPlano.Id);
        }
    }
}
