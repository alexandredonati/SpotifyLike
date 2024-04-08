using SpotifyLike.Application.Conta.Dto;
using SpotifyLike.Domain.Streaming.Aggregates;

namespace SpotifyLike.Application.Conta.Profile
{
    public class PlanoProfile : AutoMapper.Profile
    {
        public PlanoProfile()
        {
            CreateMap<PlanoDto, Plano>()
                .AfterMap((s, d) =>
                {
                    d.Id = s.Id;
                    d.Tipo = s.Tipo;
                    d.Valor.Valor = s.Valor;
                    d.Vigencia.NumDias = s.Vigencia;
                    d.Descricao = s.Descricao;
                });

            CreateMap<Plano, PlanoDto>()
                .AfterMap((s, d) =>
                {
                    d.Id = s.Id;
                    d.Tipo = s.Tipo;
                    d.Valor = s.Valor.Valor;
                    d.Vigencia = s.Vigencia.NumDias;
                    d.Descricao = s.Descricao;
                });
        }
    }
}
