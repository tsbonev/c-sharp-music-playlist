using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongPlaylistLib.Core
{
    public struct SongRequest
    {
        public SongRequest(string artist, List<Genre> genres)
        {
            Artist = artist;
            Genres = genres;
        }

        public string Artist { get; }
        public List<Genre> Genres { get; }
    }
}
