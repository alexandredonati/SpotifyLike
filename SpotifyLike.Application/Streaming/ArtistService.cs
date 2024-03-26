using AutoMapper;
using SpotifyLike.Application.Streaming.Dto;
using SpotifyLike.Domain.Streaming.Aggregates;
using SpotifyLike.Repository.Repository;

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

        public ArtistDto CreateArtist(ArtistDto dto)
        {
            Artista artista = this.Mapper.Map<Artista> (dto);
            this.ArtistRepository.Save(artista);

            return this.Mapper.Map<ArtistDto>(artista);
        }

        public ArtistDto GetArtistById(Guid id)
        {
            var artista = this.ArtistRepository.GetById(id);
            return this.Mapper.Map<ArtistDto>(artista);
        }

        public IEnumerable<ArtistDto> GetArtists()
        {
            var artistas = this.ArtistRepository.GetAll();
            return this.Mapper.Map<IEnumerable<ArtistDto>>(artistas);
        }

        public IEnumerable<AlbumDto> GetArtistAlbums(Guid IdArtista)
        {
            var artista = this.ArtistRepository.GetById(IdArtista);

            //if (artista == null)
            //    throw new Exception("Artista não encontrado.");

            var albums = artista.Albums;

            var result = this.Mapper.Map<IEnumerable<AlbumDto>>(albums);

            return result;
        }
    }
}
