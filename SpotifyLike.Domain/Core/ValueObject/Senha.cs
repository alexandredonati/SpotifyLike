using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace SpotifyLike.Domain.Core.ValueObject
{
    public class Senha
    {
        public String HexValue { get; set; }
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
