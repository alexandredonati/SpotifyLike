using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SpotifyLike.Domain.Core.ValueObject;

namespace SpotifyLike.Application.Conta.Dto
{
    public class UsuarioDto
    {
        public Guid Id { get; set; }

        [Required]
        public String Nome { get; set; }

        [Required]
        [EmailAddress]
        public String Email { get; set; }

        [Required]
        public DateTime DataNascimento { get; set; }

        [Required]
        public string Senha { get; set; }
        public Guid PlanoId { get; set; }

        [Required]
        public CartaoDto Cartao { get; set; }
        public Guid FavoritePlaylistId { get; set; }
    }
}
