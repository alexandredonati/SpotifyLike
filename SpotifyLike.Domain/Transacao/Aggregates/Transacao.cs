using SpotifyLike.Domain.Notificacao;
using SpotifyLike.Domain.Core.ValueObject;
using SpotifyLike.Domain.Transacao.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Domain.Transacao.Aggregates
{
    public class Transacao
    {
        private const int INTERVALO_TRANSACAO = 2;
        private const int LIMITE_TRANSACOES_INTERVALO = 3;
        private const int REPETICAO_TRANSACAO_MERCHANT = 1;
        public Guid Id { get; set; }
        public DateTime DataTransacao { get; set; }
        public Cartao CartaoOrigem { get; set; }
        public Merchant Recebedor { get; set; }
        public Monetario Valor { get; set; }
        public String Descricao { get; set; }
        public Boolean Autorizada { get; set; }
        public Transacao() { }
        public Transacao(Cartao origem, Merchant recebedor, Monetario valor, string descricao)
        {
            this.Id = Guid.NewGuid();
            this.DataTransacao = DateTime.Now;
            this.CartaoOrigem = origem;
            this.Recebedor = recebedor;
            this.Valor = valor;
            this.Descricao = descricao;
            this.Autorizada = false;
        }

        public void Autorizar()
        {
            this.CartaoOrigem.IsCartaoAtivo();
            this.VerificaLimite();
            this.ValidarFrequencia();

            this.Autorizada = true;
        }

        public void Notificar()
        {
            Notificacao.Notificacao NovaNotificacao = Notificacao.Notificacao.Criar(
                titulo: "Transação realizada!",
                mensagem: $"A transação {this.Id} foi realizada com sucesso no valor de R${this.Valor.Valor}. Descrição da transação: {this.Descricao}",
                tipoNotificacao: Notificacao.TipoNotificacao.Sistema,
                destinatario: this.CartaoOrigem.Proprietario
                );
        }
        private void VerificaLimite()
        {
            if (this.CartaoOrigem.Limite < this.Valor)
                throw new Exception("O cartão não possui limite para esta transação.");
        }

        private void ValidarFrequencia()
        {
            var ultimasTransacoes = this.CartaoOrigem.Transacoes.Where(x =>
                                                          x.DataTransacao >= DateTime.Now.AddMinutes(-INTERVALO_TRANSACAO));

            if (ultimasTransacoes?.Count() >= LIMITE_TRANSACOES_INTERVALO)
                throw new Exception(
                    String.Format(
                        "Cartão utilizado mais de {0} vez{1} em um período de {2} minuto{3}",
                        LIMITE_TRANSACOES_INTERVALO,
                        LIMITE_TRANSACOES_INTERVALO > 1 ? "es" :"",
                        INTERVALO_TRANSACAO,
                        INTERVALO_TRANSACAO > 1 ? "s" : ""
                    ));

            var transacaoRepetidaPorMerchant = ultimasTransacoes?
                                                .Where(x => x.Recebedor.Nome.ToUpper() == this.Recebedor.Nome.ToUpper()
                                                       && x.Valor.Valor == this.Valor.Valor)?
                                                .Count() > REPETICAO_TRANSACAO_MERCHANT;

            if (transacaoRepetidaPorMerchant)
                throw new Exception("Transacao Duplicada para o mesmo cartão e o mesmo Comerciante");

        }

    }
}
