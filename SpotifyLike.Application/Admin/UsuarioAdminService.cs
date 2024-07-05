using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SpotifyLike.Application.Admin.Dto;
using SpotifyLike.Domain.Admin.Aggregates;
using SpotifyLike.Domain.Admin.ValueObject;
using SpotifyLike.Repository.Repository;

namespace SpotifyLike.Application.Admin
{
    public class UsuarioAdminService
    {
        private UsuarioAdminRepository Repository { get; set; }
        private IMapper mapper { get; set; }
        public UsuarioAdminService(UsuarioAdminRepository repository, IMapper mapper)
        {
            Repository = repository;
            this.mapper = mapper;
        }

        public IEnumerable<UsuarioAdminDto> GetAll()
        {
            var result = this.Repository.GetAll();

            return this.mapper.Map<IEnumerable<UsuarioAdminDto>>(result);
        }

        public void Salvar(UsuarioAdminDto dto)
        {
            var usuario = this.mapper.Map<UsuarioAdmin>(dto);
            this.Repository.Save(usuario);
        }

        public UsuarioAdmin Authenticate(string email, string password)
        {
            var passwordHash = new Senha(password).HexValue;
            return this.Repository.GetByEmailAndPassword(email, passwordHash);
        }
    };
}
