using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Application.Conta
{
    public class Notificacao
    {
        public Guid IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Mensagem { get; set; }
    }
}
