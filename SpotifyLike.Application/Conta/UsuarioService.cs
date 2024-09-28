using AutoMapper;
using SpotifyLike.Application.Conta.Dto;
using SpotifyLike.Application.Streaming.Dto;
using SpotifyLike.Domain;
using SpotifyLike.Domain.Conta.Aggregates;
using SpotifyLike.Domain.Core.ValueObject;
using SpotifyLike.Domain.Streaming.Aggregates;
using SpotifyLike.Domain.Transacao.Aggregates;
using SpotifyLike.Repository.Repository;

namespace SpotifyLike.Application.Conta
{
    public class UsuarioService
    {
        private IMapper Mapper { get; set; }
        private UsuarioRepository UsuarioRepository { get; set; }
        private PlanoRepository PlanoRepository { get; set; }
        private AssinaturaRepository AssinaturaRepository { get; set; }
        private TransacaoRepository TransacaoRepository { get; set; }
        private SongRepository SongRepository { get; set; }
        private AzureServiceBusService ServiceBusService { get; set; }

        public UsuarioService(IMapper mapper, UsuarioRepository usuarioRepository, PlanoRepository planoRepository, AssinaturaRepository assinaturaRepository, TransacaoRepository transacaoRepository, SongRepository songRepository, AzureServiceBusService serviceBusService)
        {
            Mapper = mapper;
            UsuarioRepository = usuarioRepository;
            PlanoRepository = planoRepository;
            AssinaturaRepository = assinaturaRepository;
            TransacaoRepository = transacaoRepository;
            SongRepository = songRepository;
            ServiceBusService = serviceBusService;
        }

        public async Task<UsuarioDto> Create(UsuarioDto dto)
        {
            if (this.UsuarioRepository.Exists(x => x.Email == dto.Email))
                throw new BusinessRuleException("Usuário ja existente na base.");

            Plano plano = this.PlanoRepository.GetById(dto.PlanoId);

            if (plano == null)
                throw new BusinessRuleException("Plano não existente ou não encontrado.");

            Cartao cartao = this.Mapper.Map<Cartao>(dto.Cartao);

            Usuario usuario = new Usuario();
            usuario.CriarConta(dto.Nome, dto.Email, dto.Senha, dto.DataNascimento, cartao, plano);

            this.UsuarioRepository.Save(usuario);
            var result = this.Mapper.Map<UsuarioDto>(usuario);

            //Notificar Usuário
            var notificacao = new Notificacao
            {
                IdUsuario = usuario.Id,
                Nome = usuario.Nome,
                Mensagem = $"Bem vindo ao SpotifyLike, {usuario.Nome}"
            };

            await this.ServiceBusService.SendMessage(notificacao);

            return result;

        }

        public UsuarioDto UpdateSubscription(SubscriptionDto dto)
        {
            var usuario = this.UsuarioRepository.GetById(dto.UserId);
            if (null == usuario)
                throw new BusinessRuleException("Usuário não encontrado.");

            Plano plano = this.PlanoRepository.GetById(dto.PlanoId);
            if (null == plano)
                throw new BusinessRuleException("Plano não existente ou não encontrado.");

            if (usuario.Assinaturas.FirstOrDefault(a => a.Ativo)?.Plano.Id == plano.Id)
            {
                throw new BusinessRuleException("A assinatura atual do usuário já contempla o plano selecionado.");
            }

            var cartao = usuario.Cartoes.Where(x => x.Ativo).FirstOrDefault();
            if (null == cartao)
                throw new BusinessRuleException("Usuário não possui cartão ativo.");

            (var novaAssinatura, var novaTransacao) = usuario.AtualizarAssinatura(plano, cartao);

            this.AssinaturaRepository.Save(novaAssinatura);
            this.TransacaoRepository.Save(novaTransacao);

            this.UsuarioRepository.Update(usuario);
            var result = this.Mapper.Map<UsuarioDto>(usuario);

            return result;
        }
        public UsuarioDto GetUserById(Guid id)
        {
            var usuario = this.UsuarioRepository.GetById(id);
            var result = this.Mapper.Map<UsuarioDto>(usuario);
            return result;
        }
        public IEnumerable<UsuarioDto> GetUsers()
        {
            var usuarios = this.UsuarioRepository.GetAll();
            var result = this.Mapper.Map<IEnumerable<UsuarioDto>>(usuarios);
            return result;
        }

        public IEnumerable<SongDto> GetFavoritas(Guid id)
        {
            var usuario = this.UsuarioRepository.GetById(id);
            if (usuario == null)
                throw new BusinessRuleException("Usuário não encontrado.");

            var playlistFavoritas = usuario.Playlists.FirstOrDefault(x => x.Id == usuario.FavoritePlaylistId);

            return this.Mapper.Map<IEnumerable<SongDto>>(playlistFavoritas?.Musicas);
        }

        public IEnumerable<PlaylistDto> GetPlaylists(Guid id)
        {
            var usuario = this.UsuarioRepository.GetById(id);
            if (usuario == null)
                throw new BusinessRuleException("Usuário não encontrado.");

            var playlists = usuario.Playlists.Where(x => x.Id != usuario.FavoritePlaylistId);
            return this.Mapper.Map<IEnumerable<PlaylistDto>>(playlists);
        }

        public void TestMethod()
        {
            var novoCartao = new Cartao
            {
                Ativo = true,
                Numero = "123",
                DataVencimento = DateTime.Now,
                Limite = new Domain.Core.ValueObject.Monetario { Valor = 123 },
                Transacoes = new List<Transacao>()
            };
            var novaTransacao = new Transacao
            {
                Descricao = "teste",
                DataTransacao = DateTime.Now,
                Autorizada = true,
                Recebedor = new Domain.Transacao.ValueObject.Merchant { Nome = "zé" },
                Valor = new Domain.Core.ValueObject.Monetario { Valor = 123 },
                CartaoOrigem = novoCartao
            };
            var novoUsuario = new Usuario
            {
                Nome = "",
                Email = "",
                DataNascimento = DateTime.Today,
                Senha = new Domain.Core.ValueObject.Senha { HexValue = "" },
                Cartoes = new List<Cartao>() { novoCartao }
            };

            this.TransacaoRepository.Save(novaTransacao);
            //this.UsuarioRepository.Save(novoUsuario);
        }

        public async Task<UsuarioDto> Authenticate(string email, string senha)
        {
            var senhaHex = new Senha(senha);
            var user = this.UsuarioRepository.Find(x => x.Email == email && x.Senha.HexValue == senhaHex.HexValue).FirstOrDefault();
            if (user == null)
                throw new BusinessRuleException("Usuário e/ou senha inválido(s).");

            var result = this.Mapper.Map<UsuarioDto>(user);

            //Notificar Usuário
            var notificacao = new Notificacao
            {
                IdUsuario = user.Id,
                Nome = user.Nome,
                Mensagem = $"Alerta: {user.Nome} acabou de fazer o login às {DateTime.Now}"
            };

            await this.ServiceBusService.SendMessage(notificacao);

            return result;        
        }

        public UsuarioDto FavoritarMusica(Guid idUser, Guid idSong)
        {
            var usuario = this.UsuarioRepository.GetById(idUser);
            if (usuario == null)
                throw new BusinessRuleException("Usuário não encontrado.");

            var musica = this.SongRepository.GetById(idSong);
            if (musica == null)
                throw new BusinessRuleException("Música não encontrada.");

            usuario.FavoritarMusica(musica);

            this.UsuarioRepository.Update(usuario);
            return this.Mapper.Map<UsuarioDto>(usuario);
        }

        public UsuarioDto DesfavoritarMusica(Guid idUser, Guid idSong)
        {
            var usuario = this.UsuarioRepository.GetById(idUser);
            if (usuario == null)
                throw new BusinessRuleException("Usuário não encontrado.");

            var musica = this.SongRepository.GetById(idSong);
            if (musica == null)
                throw new BusinessRuleException("Música não encontrada.");

            usuario.DesfavoritarMusica(musica);

            this.UsuarioRepository.Update(usuario);
            return this.Mapper.Map<UsuarioDto>(usuario);
        }

        public IEnumerable<Guid> GetAllFavoritePlaylistsIds()
        {
            var usuarios = GetUsers();

            return usuarios.Select(usuario => usuario.FavoritePlaylistId);
        }

        public SubscriptionDto GetSubscription(Guid userId)
        {
            var usuario = this.UsuarioRepository.GetById(userId);
            if (usuario == null)
                throw new BusinessRuleException("Usuário não encontrado.");

            var assinatura = usuario.Assinaturas.FirstOrDefault(a => a.Ativo);
            if (assinatura == null)
                throw new BusinessRuleException("Usuário não possui assinatura ativa.");

            return this.Mapper.Map<SubscriptionDto>(assinatura);
        }

    }
}
