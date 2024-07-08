using AutoMapper;
using SpotifyLike.Application.Streaming.Dto;
using SpotifyLike.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Application.Streaming
{
    public class PlaylistService
    {
        private PlaylistRepository Repository { get; set; }

        private IMapper mapper { get; set; }

        public PlaylistService(PlaylistRepository repository, IMapper mapper)
        {
            Repository = repository;
            this.mapper = mapper;
        }
       
        public PlaylistDto GetById(Guid id)
        {
            var playlist = Repository.GetById(id);
            return mapper.Map<PlaylistDto>(playlist);
        }

    }
}
