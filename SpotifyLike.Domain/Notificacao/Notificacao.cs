using SpotifyLike.Domain.Conta.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Domain.Notificacao
{
    public class Notificacao
    {
        public Guid Id { get; set; }
        public virtual Usuario Remetente { get; set; }
        public virtual Usuario Destinatario { get; set; }
        public string Mensagem { get; set; }
        public string Titulo { get; set; }
        public DateTime Instante { get; set; }
        public TipoNotificacao TipoNotificacao { get; set; }

        public static Notificacao Criar(string titulo, string mensagem, TipoNotificacao tipoNotificacao, Usuario destinatario, Usuario remetente = null)
        {
            if (tipoNotificacao == TipoNotificacao.Usuario && remetente == null)
                throw new ArgumentNullException("Para tipo de mensagem 'usuário', você deve informar quem foi o remetente");

            if (string.IsNullOrWhiteSpace(titulo))
                throw new ArgumentNullException("Informe o titulo da notificacao");

            if (string.IsNullOrWhiteSpace(mensagem))
                throw new ArgumentNullException("Informe o mensagem da notificacao");

            Notificacao NovaNotificacao = new Notificacao()
            {
                Instante = DateTime.Now,
                Mensagem = mensagem,
                TipoNotificacao = tipoNotificacao,
                Titulo = titulo,
                Destinatario = destinatario,
                Remetente = remetente
            };

            destinatario.Notificacoes.Add(NovaNotificacao);

            return NovaNotificacao;
        }
    }

    public enum TipoNotificacao
    {
        Usuario = 1,
        Sistema = 2
    }
}
