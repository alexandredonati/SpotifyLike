using SpotifyLike.Domain.Core.ValueObject;
using SpotifyLike.Domain.Streaming.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Application.Conta.Dto
{
    public class PlanoDto
    {
        public Guid Id { get; set; }
        public string Tipo { get; set; }
        public decimal Valor { get; set; }
        public int Vigencia { get; set; }
        public string Descricao { get; set; }
    }
}
