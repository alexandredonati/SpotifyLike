using System.ComponentModel.DataAnnotations;

namespace SpotifyLike.Admin.Models
{
    public class LoginRequest
    {
        [Required(ErrorMessage ="O campo Email é obrigatório")]
        [EmailAddress(ErrorMessage ="O campo Email é inválido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo Senha é obrigatório")]
        public string Senha { get; set; }
    }
}
