using System.ComponentModel.DataAnnotations;

namespace SpotifyLike.Application.Conta.Dto
{
    public class SubscriptionDto
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid PlanoId { get; set; }
    }
}
