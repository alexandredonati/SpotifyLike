using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Domain.Streaming.ValueObject
{
    public class Duracao
    {
        public int Valor { get; set; }

        public Duracao(int valor) 
        {
            if (valor < 0)
                throw new ArgumentException("Duração da música não pode ser negativa");

            this.Valor = valor;
        }

        public String Formatado() 
        {
            int minutos = this.Valor / 60;
            int segundos = this.Valor % 60;

            return $"{minutos.ToString().PadLeft(1, '0')}:{segundos.ToString().PadLeft(1, '0')}";
        }

        public static implicit operator Duracao(int valor) { return new Duracao(valor);}
        public static implicit operator int(Duracao valor) { return valor.Valor;}
    }
}
