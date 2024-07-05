using SpotifyLike.Domain.Admin.Aggregates;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Application.Admin.Dto
{
    public class UsuarioAdminDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo Email é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo Email é inválido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo Senha é obrigatório")]
        public string Senha { get; set; }
        [Required(ErrorMessage = "O campo Perfil é obrigatório")]
        public int Perfil { get; set; }
    }
}
