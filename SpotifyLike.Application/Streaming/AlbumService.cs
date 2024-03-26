using AutoMapper;
using SpotifyLike.Application.Streaming.Dto;
using SpotifyLike.Domain;
using SpotifyLike.Domain.Streaming.Aggregates;
using SpotifyLike.Repository.Repository;

namespace SpotifyLike.Application.Streaming
{
    public class AlbumService
    {
        private AlbumRepository AlbumRepository { get; set; }
        private ArtistRepository ArtistRepository { get; set; }
        private IMapper Mapper { get; set; }

        public AlbumService(AlbumRepository albumRepository, IMapper mapper, ArtistRepository artistRepository)
        {
            AlbumRepository = albumRepository;
            Mapper = mapper;
            ArtistRepository = artistRepository;
        }
        public AlbumDto CreateAlbum(AlbumDto dto)
        {
            var artists =
                    dto.ArtistIds
                        .Select(
                            artistId =>
                            {
                                var artist = this.ArtistRepository.GetById(artistId);
                                if (null != artist)
                                {
                                    var novoAlbum = this.AlbumDtoToAlbum(dto);
                                    artist.AdicionarAlbum(novoAlbum);
                                }
                                return (artistId, artist);
                            }
                        );
            var artistIdsNotFound =
                    artists
                        .Where(x => x.artist is not null)
                        .Select(x => x.artistId);
            if (artistIdsNotFound.Count() > 0)
            {
                throw new BusinessRuleException(
                    String.Format(
                        "ID(s) de Artista(s) não encontrada(s): {0}. Primeiro, é necessário adicionar todos os artistas do álbum.",
                        String.Join(", ", artistIdsNotFound))
                );
            }
            {
                foreach (Artista artist in artists.Select(x => x.artist))
                {
                    this.ArtistRepository.Update(artist);
                }
            }
            var outputAlbum = this.AlbumRepository.GetById(dto.Id);
            var result = this.AlbumToAlbumDto(outputAlbum);
            return result;
        }
        public AlbumDto GetAlbumById(Guid id)
        {
            var album = this.AlbumRepository.GetById(id);
            //if (album == null)
            //    throw new BusinessRuleException("Album não encontrado.");
            var result = AlbumToAlbumDto(album);
            return result;
        }

        public Album AlbumDtoToAlbum(AlbumDto dto)
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

        public AlbumDto AlbumToAlbumDto(Album album)
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
