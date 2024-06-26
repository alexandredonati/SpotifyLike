﻿using SpotifyLike.Domain.Streaming.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Repository.Repository
{
    public class AlbumRepository : RepositoryBase<Album>
    {
        public AlbumRepository(SpotifyLikeContext context) : base(context) 
        {
        }
    }
}
