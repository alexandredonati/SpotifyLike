using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Domain.Streaming.ValueObject
{
    public class Periodo
    {
        public int NumDias { get; set; }

        public Periodo() { }
        public Periodo(int dias)
        {
            if (dias <= 0)
                throw new ArgumentException("Número de dias não pode ser nulo ou negativo.");

            NumDias = dias; 
        }
    }
}
