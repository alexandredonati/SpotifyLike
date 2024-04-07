using SpotifyLike.Application.Conta.Dto;
using SpotifyLike.Domain.Conta.Aggregates;
using SpotifyLike.Domain.Core.ValueObject;
using SpotifyLike.Domain.Transacao.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Application.Conta.Profile
{
    public class UsuarioProfile: AutoMapper.Profile
    {
        public UsuarioProfile()
        {
            CreateMap<UsuarioDto, Usuario>()
                .AfterMap((s, d) => 
                {
                    d.Senha = new Senha(s.Senha);
                });

            CreateMap<Usuario, UsuarioDto>()
                .AfterMap((s, d) =>
                {
                    var plano = s.Assinaturas?.FirstOrDefault(a => a.Ativo)?.Plano;

                    if (plano != null)
                        d.PlanoId = plano.Id;

                    d.Senha = "********";

                    var activeCard = s.Cartoes.FirstOrDefault(x => x.Ativo);

                    var cartaoDto = new CartaoDto();
                    if (activeCard != null)
                    {
                        cartaoDto.Id = "**********";
                        cartaoDto.Ativo = true;
                        cartaoDto.Numero = "**** **** **** " + activeCard.Numero.Substring(activeCard.Numero.Length - 4);
                    }

                    else
                        cartaoDto.Numero = "Nenhum cartão ativo";

                    d.Cartao = cartaoDto;
                });

            CreateMap<CartaoDto, Cartao>()
                .ForPath(x => x.Limite.Valor, m => m.MapFrom(f => f.Limite))
                .ReverseMap();
        }
    }
}
