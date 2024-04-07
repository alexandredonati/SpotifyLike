using System.ComponentModel.DataAnnotations;

namespace SpotifyLike.Api.Controllers.Request
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "O campo E-mail é obrigatório.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        public string Senha { get; set; }
    }
}
