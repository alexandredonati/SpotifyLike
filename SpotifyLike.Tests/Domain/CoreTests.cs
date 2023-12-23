using SpotifyLike.Domain.Core.ValueObject;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Tests.Domain
{
    public class CoreTests
    {
        [Fact]
        public void DeveCriarCnpjComSucesso()
        {
            String strCnpj = "36.419.737/0001-90";
            strCnpj = strCnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            Cnpj cnpj = new Cnpj(strCnpj);

            Assert.True(cnpj.Valor == strCnpj);
        }

        [Fact]
        public void NaoDeveCriarCnpjInvalido()
        {
            String strCnpj = "48251111101642";

            Assert.Throws<Exception>(() =>
            {
                Cnpj cnpj = new Cnpj(strCnpj);
            });
        }

        [Fact]
        public void NaoDeveCriarCnpjCaracteresInvalidos()
        {
            String strCnpj = "30 631_540(0001)34";

            Assert.Throws<Exception>(() =>
            {
                Cnpj cnpj = new Cnpj(strCnpj);
            });
        }

        [Fact]
        public void DeveCriarMonetarioComSucesso()
        {
            Decimal valor = 23.50M;
            Monetario monetario = new Monetario(valor);

            Assert.True(monetario.Valor == valor);
        }

        [Fact]
        public void NaoDeveCriarMonetarioNegativo()
        {
            Decimal valor = -11.90M;

            Assert.Throws<ArgumentException>(() =>
            {
                Monetario monetario = new Monetario(valor);
            });
        }
    }
}
