using Newtonsoft.Json;

namespace SpotifyLike.Domain.Admin.Aggregates
{
    public class UsuarioAdmin : IIdentifier
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("nome")]
        public string Nome { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("senha")]
        public string Senha { get; set; }
        [JsonProperty("perfil")]
        public Perfil Perfil { get; set; }
        [JsonProperty("partitionKey")]
        public string PartitionKey = "UsuarioAdminPartition";

    }
}
