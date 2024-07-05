using SpotifyLike.Application.Admin.Dto;
using SpotifyLike.Domain.Admin.Aggregates;
using SpotifyLike.Domain.Admin.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Application.Admin.Profile
{
    public class UsuarioAdminProfile: AutoMapper.Profile
    {
        public UsuarioAdminProfile()
        {
            CreateMap<UsuarioAdminDto, UsuarioAdmin >()
                .ForMember(x => x.Perfil, m => m.MapFrom(f => (Perfil)f.Perfil))
                .AfterMap((s, d) =>
                {
                    d.Senha = new Senha(s.Senha).HexValue;
                })
                .ReverseMap();
                
        }   
    }
}
