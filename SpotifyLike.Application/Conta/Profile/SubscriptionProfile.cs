using SpotifyLike.Application.Conta.Dto;
using SpotifyLike.Domain.Conta.Aggregates;
using SpotifyLike.Domain.Streaming.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Application.Conta.Profile
{
    public class SubscriptionProfile : AutoMapper.Profile
    {
        public SubscriptionProfile()
        {
            CreateMap<Assinatura, SubscriptionDto>()
                .AfterMap((s, d) =>
                {
                    d.Id = s.Id;
                    d.UserId = s.UsuarioId;
                    d.PlanoId = s.Plano.Id;
                    d.DataInicio = s.DataInicio;
                    d.DataVencimento = s.DataVencimento;
                }).ReverseMap();
        }
    }
}
