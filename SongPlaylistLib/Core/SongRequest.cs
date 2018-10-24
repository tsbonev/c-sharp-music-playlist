using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongPlaylistLib.Core
{
    public struct SongRequest
    {
        public SongRequest(string artist, List<string> genres)
        {
            Artist = artist;
            Genres = genres.ConvertAll(s => s.ToLower());
        }

        public string Artist { get; }
        public List<string> Genres { get; }
    }
}
