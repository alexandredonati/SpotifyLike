using AutoMapper;
using SpotifyLike.Application.Streaming.Dto;
using SpotifyLike.Domain;
using SpotifyLike.Domain.Streaming.Aggregates;
using SpotifyLike.Repository.Repository;

namespace SpotifyLike.Application.Streaming
{
    public class ArtistService
    {
        private ArtistRepository ArtistRepository { get; set; }
        private IMapper Mapper { get; set; }
        private AzureStorageAccount AzureStorageAccount { get; set; }

        public ArtistService(ArtistRepository artistRepository, IMapper mapper, AzureStorageAccount azureStorageAccount)
        {
            ArtistRepository = artistRepository;
            Mapper = mapper;
            AzureStorageAccount = azureStorageAccount;
        }

        public async Task<ArtistDto> CreateArtist(ArtistDto dto)
        {
            Artista artista = this.Mapper.Map<Artista> (dto);

            var urlBackdrop = await this.AzureStorageAccount.UploadImage(dto.Backdrop);
            artista.Backdrop = urlBackdrop;

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

            if (artista == null)
                throw new BusinessRuleException("Artista não encontrado.");

            var albums = artista.Albums;

            var result = this.Mapper.Map<IEnumerable<AlbumDto>>(albums);

            return result;
        }

        public IEnumerable<SongDto> GetArtistSongs(Guid IdArtista)
        {
            var artista = this.ArtistRepository.GetById(IdArtista);

            if (artista == null)
                throw new BusinessRuleException("Artista não encontrado.");

            var musicas = artista.Musicas;

            var result = this.Mapper.Map<IEnumerable<SongDto>>(musicas);

            return result;
        }

        public IEnumerable<ArtistDto> SearchArtists(string searchText)
        {
            var artistas = this.ArtistRepository
                            .GetAll()
                            .Where(a => a.Nome.ToLower().Contains(searchText.ToLower()));

            return this.Mapper.Map<IEnumerable<ArtistDto>>(artistas);
        }

        public void DeleteArtist(Guid id)
        {
            var artista = this.ArtistRepository.GetById(id);
            if (artista == null)
                throw new BusinessRuleException("Artista não encontrado.");

            this.ArtistRepository.Delete(artista);
        }   
    }
}
