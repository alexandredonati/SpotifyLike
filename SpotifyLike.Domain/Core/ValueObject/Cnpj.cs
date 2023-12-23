using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Domain.Core.ValueObject
{
    public class Cnpj
    {
        public String Valor {  get; set; }

        public Cnpj(string valor) 
        {
            ValidateCnpj(valor);

            this.Valor = valor;
        }

        public void ValidateCnpj(string valor) 
        {
            int[] Multiplicadores1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] Multiplicadores2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            
            int Soma;
            int Resto;
            string Digito;
            string TempCnpj;

            valor = valor.Trim();
            valor = valor.Replace(".", "").Replace("-", "").Replace("/", "");

            if (valor.Length != 14)
                throw new Exception("O CNPJ deve conter 14 digitos. Os únicos separadores permitidos são '.', '-' e '/'.");

            TempCnpj = valor.Substring(0, 12);

            Soma = 0;
            for (int i = 0; i < 12; i++)
                Soma += int.Parse(TempCnpj[i].ToString()) * Multiplicadores1[i];

            Resto = (Soma % 11);
            if (Resto < 2)
                Resto = 0;
            else
                Resto = 11 - Resto;

            Digito = Resto.ToString();

            TempCnpj = TempCnpj + Digito;

            Soma = 0;
            for (int i = 0; i < 13; i++)
                Soma += int.Parse(TempCnpj[i].ToString()) * Multiplicadores2[i];

            Resto = (Soma % 11);
            if (Resto < 2)
                Resto = 0;
            else
                Resto = 11 - Resto;

            Digito = Digito + Resto.ToString();
            
            TempCnpj = TempCnpj + Digito;

            if (valor != TempCnpj)
                throw new Exception($"O Cnpj {valor} não é válido.");
        }
    }
}
