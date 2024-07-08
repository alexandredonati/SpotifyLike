using AutoMapper;
using SpotifyLike.Application.Streaming.Dto;
using SpotifyLike.Domain;
using SpotifyLike.Domain.Streaming.Aggregates;
using SpotifyLike.Repository.Repository;

namespace SpotifyLike.Application.Streaming
{
    public class AlbumService
    {
        private AlbumRepository _albumRepository { get; set; }
        private ArtistRepository _artistRepository { get; set; }
        private SongRepository _songRepository { get; set; }
        private IMapper Mapper { get; set; }

        public AlbumService(AlbumRepository albumRepository, IMapper mapper, ArtistRepository artistRepository, SongRepository songRepository)
        {
            _albumRepository = albumRepository;
            Mapper = mapper;
            _artistRepository = artistRepository;
            _songRepository = songRepository;
        }
        public AlbumDto CreateAlbum(AlbumDto dto)
        {
            var novoAlbum = this.AlbumDtoToAlbum(dto);
            var artists =
                    dto.ArtistIds
                        .Select(
                            artistId =>
                            {
                                var artist = this._artistRepository.GetById(artistId);
                                if (null != artist)
                                {
                                    artist.AdicionarAlbum(novoAlbum);
                                }
                                return (artistId, artist);
                            }
                        );
            var artistIdsNotFound =
                    artists
                        .Where(x => x.artist is null)
                        .Select(x => x.artistId);
            if (artistIdsNotFound.Count() > 0)
            {
                throw new BusinessRuleException(
                    String.Format(
                        "ID(s) de Artista(s) não encontrada(s): {0}. Primeiro, é necessário adicionar todos os artistas do álbum.",
                        String.Join(", ", artistIdsNotFound))
                );
            }
            //foreach (Artista artist in artists.Select(x => x.artist))
            //{
            //    this.ArtistRepository.Update(artist);
            //}
            this._albumRepository.Save(novoAlbum);
            //var outputAlbum = this.AlbumRepository.GetById(dto.Id);
            //var result = this.AlbumToAlbumDto(outputAlbum);
            var result = this.AlbumToAlbumDto(novoAlbum);
            return result;
        }
        public AlbumDto AddSongToAlbum(SongDto songDto)
        {
            var album = this._albumRepository.GetById(songDto.AlbumId);
            if (null == album)
            {
                throw new BusinessRuleException("Album não encontrado.");
            }
            var musica = SongDtoToMusica(songDto);
            album.AdicionarMusica(musica);
            this._albumRepository.Update(album);
            var result = this.AlbumToAlbumDto(album);
            return result;
        }

        public AlbumDto AddSongsToAlbum(Guid albumId, IList<SongDto> musicDto)
        {
            var album = this._albumRepository.GetById(albumId);
            if (null == album)
            {
                throw new BusinessRuleException("Album não encontrado.");
            }
            var musicas = this.Mapper.Map<IList<Musica>>(musicDto);
            album.AdicionarMusicas(musicas);
            this._albumRepository.Update(album);
            var result = this.AlbumToAlbumDto(album);
            return result;

        }

        public IEnumerable<AlbumDto> GetAllAlbums()
        {
            var albums = this._albumRepository.GetAll();
            var result = albums.Select(album => AlbumToAlbumDto(album));
            return result;
        }
        public AlbumDto GetAlbumById(Guid id)
        {
            var album = this._albumRepository.GetById(id);
            if (album == null)
                throw new BusinessRuleException("Album não encontrado.");
            var result = AlbumToAlbumDto(album);
            return result;
        }

        public IEnumerable<SongDto> GetAlbumSongs(Guid albumId)
        {
            var album = this._albumRepository.GetById(albumId);
            if (album == null)
                throw new BusinessRuleException("Album não encontrado.");
            var result = album.Musicas.Select(musica => new SongDto
            {
                Id = musica.Id,
                Titulo = musica.Titulo,
                Duracao = musica.Duracao.Valor
            });
            return result;
        }

        public Album AlbumDtoToAlbum(AlbumDto dto)
        {
            Album album = new Album()
            {
                Nome = dto.Nome,
            };
            foreach (SongDto item in dto.Musicas)
            {
                album.AdicionarMusica(SongDtoToMusica(item));
            }
            return album;
        }

        public AlbumDto AlbumToAlbumDto(Album album)
        {
            AlbumDto dto = new AlbumDto()
            {
                Id = album.Id,
                Nome = album.Nome,
                ArtistIds = album.Artistas.Select(artist => artist.Id).ToList()
            };
            foreach (var item in album.Musicas)
            {
                var songDto = new SongDto()
                {
                    Id = item.Id,
                    Duracao = item.Duracao.Valor,
                    Titulo = item.Titulo
                };

                dto.Musicas.Add(songDto);
            }
            return dto;
        }

        public Musica SongDtoToMusica(SongDto dto)
        {
            return new Musica
            {
                Id = dto.Id,
                Titulo = dto.Titulo,
                Duracao = new SpotifyLike.Domain.Streaming.ValueObject.Duracao(dto.Duracao),
                //Artistas = dto.ArtistsIds.Select(artistId => _artistRepository.GetById(artistId)).ToList(),
                Album = _albumRepository.GetById(dto.AlbumId)
            };
        }

        public void DeleteAlbum(Guid id)
        {
            var album = this._albumRepository.GetById(id);
            if (album == null)
                throw new BusinessRuleException("Album não encontrado.");

            this._albumRepository.Delete(album);
        }
    }
}
