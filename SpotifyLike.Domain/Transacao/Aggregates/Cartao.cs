using SpotifyLike.Domain.Conta.Aggregates;
using SpotifyLike.Domain.Core.ValueObject;
using SpotifyLike.Domain.Transacao.ValueObject;

namespace SpotifyLike.Domain.Transacao.Aggregates
{
    public class Cartao : IIdentifier
    {
        public Guid Id { get; set; }
        public bool Ativo { get; set; }
        public String Numero { get; set; }
        public DateTime DataVencimento { get; set; }
        public Monetario Limite { get; set; }
        public virtual Usuario Proprietario { get; set; }
        public virtual List<Transacao> Transacoes { get; set; } = new List<Transacao>();

        public bool RealizarTransacao(Merchant recebedor, Monetario valor, string descricao = "")
        {

            Transacao transacao = new Transacao(this, recebedor, valor, descricao);

            this.Transacoes.Add(transacao);

            transacao.Autorizar();

            if (transacao.Autorizada)
            {
                //Diminui o limite com o valor da transacao
                this.Limite = this.Limite - transacao.Valor;
                //transacao.Notificar();
            }

            return transacao.Autorizada;
        }

        public void IsCartaoAtivo()
        {
            if (this.DataVencimento < DateTime.Today.AddDays(1))
            {
                this.Ativo = false;
                throw new BusinessRuleException("Cartão não é valido pois já passou sua data de vencimento");
            }

            if (this.Ativo == false)
                throw new BusinessRuleException("Cartão não está ativo");
        }
    }
}
