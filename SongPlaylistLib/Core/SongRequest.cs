using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongPlaylistLib.Core
{
    public class SongRequest
    {
        public SongRequest(string artist, List<Genre> genres)
        {
            Artist = artist;
            Genres = genres;
        }

        public string Artist { get; set; }
        public List<Genre> Genres { get; set; }
    }
}
