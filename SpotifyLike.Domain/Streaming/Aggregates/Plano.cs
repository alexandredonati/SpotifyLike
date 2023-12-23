using SpotifyLike.Domain.Core.ValueObject;
using SpotifyLike.Domain.Streaming.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Domain.Streaming.Aggregates
{
    public class Plano
    {
        public Guid Id { get; set; }
        public string Tipo { get; set; }
        public Monetario Valor { get; set; }
        public Periodo Vigencia { get; set; }
        public string Descricao { get; set; }

    }
}
