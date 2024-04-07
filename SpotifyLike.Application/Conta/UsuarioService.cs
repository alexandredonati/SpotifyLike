using AutoMapper;
using SpotifyLike.Application.Conta.Dto;
using SpotifyLike.Domain;
using SpotifyLike.Domain.Conta.Aggregates;
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
        private TransacaoRepository TransacaoRepository { get; set; }


        public UsuarioService(IMapper mapper, UsuarioRepository usuarioRepository, PlanoRepository planoRepository, TransacaoRepository transacaoRepository)
        {
            Mapper = mapper;
            UsuarioRepository = usuarioRepository;
            PlanoRepository = planoRepository;
            TransacaoRepository = transacaoRepository;
        }

        public UsuarioDto Create(UsuarioDto dto)
        {
            if (this.UsuarioRepository.Exists(x => x.Email == dto.Email))
                throw new BusinessRuleException("Usuário ja existente na base.");

            Plano plano = this.PlanoRepository.GetById(dto.PlanoId);

            if (plano == null)
                throw new BusinessRuleException("Plano não existente ou não encontrado.");

            Cartao cartao = this.Mapper.Map<Cartao>(dto.Cartao);

            Usuario usuario = new Usuario();
            usuario.CriarConta(dto.Nome, dto.Email, dto.Senha.HexValue, dto.DataNascimento, cartao, plano);

            this.UsuarioRepository.Save(usuario);
            var result = this.Mapper.Map<UsuarioDto>(usuario);

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

            usuario.AtualizarAssinatura(plano, cartao);

            //var novaTransacao = usuario.Cartoes.FirstOrDefault(x => x.Ativo).Transacoes.Last();

            //this.TransacaoRepository.Save(novaTransacao);

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

    }
}
