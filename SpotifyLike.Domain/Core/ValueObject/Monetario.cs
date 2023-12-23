using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Domain.Core.ValueObject
{
    public class Monetario
    {
        public decimal Valor { get; set; }

        public static implicit operator decimal(Monetario monetario) { return monetario.Valor; }
        public static implicit operator Monetario(decimal valor) { return new Monetario(valor); }

        public Monetario(decimal valor)
        {
            if (valor < 0)
                throw new ArgumentException("Valor monetário não pode ser negativo.");

            Valor = valor;
        }
        public string Formatado()
        {
            return $"R$ {Valor.ToString("N2")}";
        }
    }
}
