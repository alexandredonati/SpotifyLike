namespace SpotifyLike.Domain.Admin.Aggregates
{
    public class UsuarioAdmin
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public Perfil Perfil { get; set; }
    }
}
