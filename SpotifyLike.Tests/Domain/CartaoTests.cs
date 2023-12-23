using SpotifyLike.Domain.Core.ValueObject;
using SpotifyLike.Domain.Transacao.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Tests.Domain
{
    public class CartaoTests
    {

        [Fact]
        public void DeveCriarTransacaoComSucesso()
        {
            Cartao cartao = new Cartao()
            {
                Id = Guid.NewGuid(),
                Numero = "123123123",
                Ativo = true,
                DataVencimento = DateTime.Today.AddYears(1),
                Limite = 100M
            };

            var merchant = new SpotifyLike.Domain.Transacao.ValueObject.Merchant()
            {
                Cnpj = new Cnpj("63342261000100"),
                Nome = "Dummy",
                Email = "dummy@dummy.com"
            };

            cartao.RealizarTransacao(merchant, 19M, "Transacao de teste");
            Assert.True(cartao.Transacoes.Count > 0);
            Assert.True(cartao.Limite == 81M);

        }

        [Fact]
        public void NaoDeveCriarTransacaoComCartaoInativo()
        {
            Cartao cartao = new Cartao()
            {
                Id = Guid.NewGuid(),
                Numero = "123123123",
                Ativo = false,
                DataVencimento = DateTime.Today.AddYears(1),
                Limite = 100M
            };

            var merchant = new SpotifyLike.Domain.Transacao.ValueObject.Merchant()
            {
                Cnpj = new Cnpj("63342261000100"),
                Nome = "Dummy",
                Email = "dummy@dummy.com"
            };

            Assert.Throws<System.Exception>(
                () => cartao.RealizarTransacao(merchant, 19M, "Dummy Transacao"));
        }

        [Fact]
        public void NaoDeveCriarTransacaoComCartaoSemLimite()
        {
            Cartao cartao = new Cartao()
            {
                Id = Guid.NewGuid(),
                Numero = "123123123",
                Ativo = true,
                DataVencimento = DateTime.Today.AddYears(1),
                Limite = 10M
            };

            var merchant = new SpotifyLike.Domain.Transacao.ValueObject.Merchant()
            {
                Cnpj = new Cnpj("63342261000100"),
                Nome = "Dummy",
                Email = "dummy@dummy.com"
            };

            Assert.Throws<System.Exception>(
                () => cartao.RealizarTransacao(merchant, 19M, "Dummy Transacao"));
        }

        [Fact]
        public void NaoDeveCriarTransacaoComCartaoValorDuplicado()
        {
            Cartao cartao = new Cartao()
            {
                Id = Guid.NewGuid(),
                Numero = "123123123",
                Ativo = true,
                DataVencimento = DateTime.Today.AddYears(1),
                Limite = 1000M
            };

            cartao.Transacoes.Add(new SpotifyLike.Domain.Transacao.Aggregates.Transacao(
                origem: cartao,
                recebedor: new SpotifyLike.Domain.Transacao.ValueObject.Merchant()
                {
                    Cnpj = new Cnpj("63342261000100"),
                    Nome = "Dummy",
                    Email = "dummy@dummy.com"
                },
                valor: 19M,
                descricao: "transação 1"
                ));

            var merchant = new SpotifyLike.Domain.Transacao.ValueObject.Merchant()
            {
                Cnpj = new Cnpj("63342261000100"),
                Nome = "Dummy",
                Email = "dummy@dummy.com"
            };

            Assert.Throws<System.Exception>(
                () => cartao.RealizarTransacao(merchant, 19M, "transação 2"));

        }

        [Fact]
        public void NaoDeveCriarTransacaoComCartaoAltoFrequencia()
        {
            Cartao cartao = new Cartao()
            {
                Id = Guid.NewGuid(),
                Numero = "123123123",
                Ativo = true,
                DataVencimento = DateTime.Today.AddYears(1),
                Limite = 1000M
            };

            cartao.Transacoes.Add(new SpotifyLike.Domain.Transacao.Aggregates.Transacao(
                origem: cartao,
                recebedor: new SpotifyLike.Domain.Transacao.ValueObject.Merchant()
                {
                    Cnpj = new Cnpj("63342261000100"),
                    Nome = "Dummy",
                    Email = "dummy@dummy.com"
                },
                valor: 19M,
                descricao: "transação 1"
                ));

            cartao.Transacoes.Add(new SpotifyLike.Domain.Transacao.Aggregates.Transacao(
                origem: cartao,
                recebedor: new SpotifyLike.Domain.Transacao.ValueObject.Merchant()
                {
                    Cnpj = new Cnpj("47.408.293/0001-04"),
                    Nome = "Dummy 2",
                    Email = "dummy2@dummy.com"
                },
                valor: 19M,
                descricao: "transação 2"
                ));

            cartao.Transacoes.Add(new SpotifyLike.Domain.Transacao.Aggregates.Transacao(
                origem: cartao,
                recebedor: new SpotifyLike.Domain.Transacao.ValueObject.Merchant()
                {
                    Cnpj = new Cnpj("75384079000109"),
                    Nome = "Dummy 3",
                    Email = "dummy3@dummy.com"
                },
                valor: 19M,
                descricao: "transação 3"
                ));


            var merchant = new SpotifyLike.Domain.Transacao.ValueObject.Merchant()
            {
                Cnpj = new Cnpj("52495878000149"),
                Nome = "Dummy 4",
                Email = "dummy4@dummy.com"
            };

            Assert.Throws<System.Exception>(
                () => cartao.RealizarTransacao(merchant, 19M, "transação 4"));
        }

        [Fact]
        public void NaoDeveCriarTransacaoComCartaoVencido()
        {
            Cartao cartao = new Cartao()
            {
                Id = Guid.NewGuid(),
                Numero = "123123123",
                Ativo = false,
                DataVencimento = DateTime.Today.AddDays(-1),
                Limite = 100M
            };

            var merchant = new SpotifyLike.Domain.Transacao.ValueObject.Merchant()
            {
                Cnpj = new Cnpj("63342261000100"),
                Nome = "Dummy",
                Email = "dummy@dummy.com"
            };

            Assert.Throws<System.Exception>(
                () => cartao.RealizarTransacao(merchant, 19M, "Dummy Transacao"));
        }

    }
}
