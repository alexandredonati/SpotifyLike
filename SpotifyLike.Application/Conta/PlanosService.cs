using AutoMapper;
using SpotifyLike.Application.Conta.Dto;
using SpotifyLike.Application.Streaming.Dto;
using SpotifyLike.Domain.Streaming.Aggregates;
using SpotifyLike.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Application.Conta
{
    public class PlanosService
    {
        private IMapper Mapper { get; set; }
        private PlanoRepository PlanoRepository { get; set; }
        private TransacaoRepository TransacaoRepository { get; set; }


        public PlanosService(IMapper mapper, PlanoRepository planoRepository, TransacaoRepository transacaoRepository)
        {
            Mapper = mapper;
            PlanoRepository = planoRepository;
            TransacaoRepository = transacaoRepository;
        }

        public IEnumerable<PlanoDto> GetPlanos()
        {
            var planos = this.PlanoRepository.GetAll();

            //return this.Mapper.Map<IEnumerable<PlanoDto>>(planos);

            return planos.Select(p => PlanoToPlanoDto(p));
        }

        public PlanoDto PlanoToPlanoDto(Plano plano)
        {
            return new PlanoDto()
                    {
                        Id = plano.Id,
                        Tipo = plano.Tipo,
                        Valor = plano.Valor.Valor,
                        Vigencia = plano.Vigencia.NumDias,
                        Descricao = plano.Descricao
                    };
        }
    }
}
