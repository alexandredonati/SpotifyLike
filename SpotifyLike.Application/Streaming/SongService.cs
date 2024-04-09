using AutoMapper;
using SpotifyLike.Application.Streaming.Dto;
using SpotifyLike.Repository.Repository;

namespace SpotifyLike.Application.Streaming
{
    public class SongService
    {
        private SongRepository songRepository { get; set; }
        private IMapper Mapper { get; set; }
        public SongService(SongRepository songRepository, IMapper mapper)
        {
            this.songRepository = songRepository;
            this.Mapper = mapper;
        }

        public IEnumerable<MusicDto> GetAllSongs()
        {
            var songs = this.songRepository.GetAll();
            return this.Mapper.Map<IEnumerable<MusicDto>>(songs);
        }

        public MusicDto GetSongById(Guid id)
        {
            var song = this.songRepository.GetById(id);
            return this.Mapper.Map<MusicDto>(song);
        }

        public IEnumerable<MusicDto> SearchSongs(string searchText)
        {
            var songs = this.songRepository
                            .GetAll()
                            .Where(s => s.Titulo.ToLower().Contains(searchText.ToLower()));

            return this.Mapper.Map<IEnumerable<MusicDto>>(songs);
        }
    }
}
