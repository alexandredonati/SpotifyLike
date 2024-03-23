using SpotifyLike.Domain.Core.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Domain.Transacao.ValueObject
{
    public record Merchant
    {
        public Cnpj Cnpj { get; set; }
        public string Nome {  get; set; }
        public string Email {  get; set; }
    }
}
