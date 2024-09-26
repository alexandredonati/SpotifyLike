using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SpotifyLike.Application.Admin.Dto;
using SpotifyLike.Application.Admin.Profile;
using SpotifyLike.Domain;
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

        public async Task<IEnumerable<UsuarioAdminDto>> GetAll()
        {
            var result = await this.Repository.ReadAllItems<UsuarioAdmin>();

            return this.mapper.Map<IEnumerable<UsuarioAdminDto>>(result);
        }

        public async Task<UsuarioAdminDto> GetById(Guid id)
        {
            var result = await this.Repository.ReadItem<UsuarioAdmin>(id.ToString());
            return this.mapper.Map<UsuarioAdminDto>(result);
        }

        public async Task<UsuarioAdmin> Salvar(UsuarioAdminDto dto)
        {
            var usuario = this.mapper.Map<UsuarioAdmin>(dto);
            usuario.Id = Guid.NewGuid();
            await this.Repository.SaveOrUpdate(usuario, usuario.PartitionKey);

            return this.mapper.Map<UsuarioAdmin>(usuario);  
        }

        public async Task<UsuarioAdmin> Authenticate(string email, string password)
        {
            var passwordHash = new Senha(password).HexValue;
            return await this.Repository.GetByEmailAndPassword(email, passwordHash);
        }

        public async void DeleteUser(Guid id)
        {
            var user = await this.Repository.ReadItem<UsuarioAdmin>(id.ToString());
            if (user == null)
                throw new BusinessRuleException("Usuário não encontrado.");

            this.Repository.Delete<UsuarioAdmin>(id.ToString(), "asd");
        }
    };
}
