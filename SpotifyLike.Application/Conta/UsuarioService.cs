﻿using AutoMapper;
using SpotifyLike.Application.Conta.Dto;
using SpotifyLike.Domain.Conta.Aggregates;
using SpotifyLike.Domain.Streaming.Aggregates;
using SpotifyLike.Domain.Transacao.Aggregates;
using SpotifyLike.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Application.Conta
{
    public class UsuarioService
    {
        private IMapper Mapper { get; set; }
        private UsuarioRepository UsuarioRepository { get; set; }
        private PlanoRepository PlanoRepository { get; set; }


        public UsuarioService(IMapper mapper, UsuarioRepository usuarioRepository, PlanoRepository planoRepository)
        {
            Mapper = mapper;
            UsuarioRepository = usuarioRepository;
            PlanoRepository = planoRepository;
        }

        public UsuarioDto Criar(UsuarioDto dto)
        {
            if (this.UsuarioRepository.Exists(x => x.Email == dto.Email))
                throw new Exception("Usuário ja existente na base.");

            Plano plano = this.PlanoRepository.GetById(dto.PlanoId);

            if (plano == null)
                throw new Exception("Plano não existente ou não encontrado.");

            Cartao cartao = this.Mapper.Map<Cartao>(dto.Cartao);

            Usuario usuario = new Usuario();
            usuario.CriarConta(dto.Nome, dto.Email, dto.Senha.HexValue, dto.DataNascimento, cartao, plano);

            //TODO: GRAVAR NA BASE DE DADOS
            this.UsuarioRepository.Save(usuario);
            var result = this.Mapper.Map<UsuarioDto>(usuario);

            return result;

        }

        public UsuarioDto Obter(Guid id)
        {
            var usuario = this.UsuarioRepository.GetById(id);
            var result = this.Mapper.Map<UsuarioDto>(usuario);
            return result;
        }
    }
}
