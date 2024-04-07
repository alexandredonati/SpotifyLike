using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Application.Conta.Dto
{
    public class CartaoDto
    {
        public string Id { get; set; }
        public Boolean Ativo { get; set; }
        public Decimal Limite { get; set; }
        public string Numero { get; set; }
        public DateTime DataVencimento { get; set; }
    }
}
