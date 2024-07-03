using System.Security.Cryptography;
using System.Text;

namespace SpotifyLike.STS.Model
{
    public class Senha
    {
        public string HexValue { get; set; }
        public Senha() { }
        public Senha(string senhaAberta) 
        {
            HexValue = CriptografarSenha(senhaAberta);
        }

        private String CriptografarSenha(string senhaAberta)
        {
            SHA256 criptoProvider = SHA256.Create();

            byte[] btexto = Encoding.UTF8.GetBytes(senhaAberta);

            var criptoResult = criptoProvider.ComputeHash(btexto);

            return Convert.ToHexString(criptoResult);
        }
    }
}
