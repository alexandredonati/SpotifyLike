using AutoMapper;
using SpotifyLike.Application.Streaming.Dto;
using SpotifyLike.Application.Streaming.Profile;
using SpotifyLike.Domain.Streaming.Aggregates;
using SpotifyLike.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Application.Streaming
{
    public class ArtistService
    {
        private ArtistRepository ArtistRepository { get; set; }
        private IMapper Mapper { get; set; }

        public ArtistService(ArtistRepository artistRepository, IMapper mapper)
        {
            ArtistRepository = artistRepository;
            Mapper = mapper;
        }

        public ArtistDto Criar(ArtistDto dto)
        {
            Artista artista = this.Mapper.Map<Artista> (dto);
            this.ArtistRepository.Save(artista);

            return this.Mapper.Map<ArtistDto>(artista);
        }

        public ArtistDto Obter(Guid id)
        {
            var artista = this.ArtistRepository.GetById(id);
            return this.Mapper.Map<ArtistDto>(artista);
        }

        public IEnumerable<ArtistDto> Obter()
        {
            var banda = this.ArtistRepository.GetAll();
            return this.Mapper.Map<IEnumerable<ArtistDto>>(banda);
        }

        public AlbumDto AssociarAlbum(AlbumDto dto)
        {
            var artista = this.ArtistRepository.GetById(dto.ArtistId);

            if (artista == null)
            {
                throw new Exception("Artista não encontrado.");
            }

            var novoAlbum = this.AlbumDtoParaAlbum(dto);

            artista.AdicionarAlbum(novoAlbum);

            this.ArtistRepository.Update(artista);

            var result = this.AlbumParaAlbumDto(novoAlbum);

            return result;

        }

        public AlbumDto ObterAlbum(Guid IdBanda, Guid id)
        {
            var artista = this.ArtistRepository.GetById(id);

            if (artista == null)
                throw new Exception("Artista não encontrado.");

            var album = artista.Albums.FirstOrDefault(x => x.Id == id);

            var result = AlbumParaAlbumDto(album);
            result.ArtistId = artista.Id;

            return result;
        }


        public Album AlbumDtoParaAlbum(AlbumDto dto)
        {
            Album album = new Album()
            {
                Nome = dto.Nome,
            };

            foreach (MusicDto item in dto.Musicas)
            {
                album.AdicionarMusica(new Musica
                {
                    Titulo = item.Nome,
                    Duracao = new SpotifyLike.Domain.Streaming.ValueObject.Duracao(item.Duracao)
                });
            }

            return album;
        }

        public AlbumDto AlbumParaAlbumDto(Album album)
        {
            AlbumDto dto = new AlbumDto()
            {
                Id = album.Id,
                Nome = album.Nome
            };

            foreach (var item in album.Musicas)
            {
                var MusicDto = new MusicDto()
                {
                    Id = item.Id,
                    Duracao = item.Duracao,
                    Nome = item.Titulo
                };

                dto.Musicas.Add(MusicDto);
            }

            return dto;
        }
    }
}
