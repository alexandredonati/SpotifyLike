using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Domain
{
    public interface IIdentifier
    {
        Guid Id { get; set; }
    }
}
