using System.ComponentModel.DataAnnotations;

namespace SpotifyLike.Application.Conta.Dto
{
    public class SubscriptionDto
    {
        public Guid Id { get; set;}
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid PlanoId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataVencimento { get; set; }

    }
}
