using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotifyLike.Domain.Streaming.Aggregates;

namespace SpotifyLike.Domain.Conta.Aggregates
{
    public class Assinatura
    {
        public Guid Id { get; set; }
        public Plano Plano { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataVencimento { get; set; }
        public Boolean Ativo { get; set; }

        public Assinatura(Plano plano)
        {
            this.Id = Guid.NewGuid();
            this.Plano = plano;
            this.DataInicio = DateTime.Now;
            this.DataVencimento = DateTime.Now.AddDays(plano.Vigencia.NumDias);
            this.Ativo = false;
        }

        public void Ativar() 
        {
            this.Ativo = true;
        }
    }
}
