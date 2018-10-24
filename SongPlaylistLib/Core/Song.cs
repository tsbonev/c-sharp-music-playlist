using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongPlaylistLib.Core
{
    public struct Song
    {
        public Song(string artist, List<string> genres)
        {
            Id = Guid.NewGuid().ToString();
            Artist = artist;
            Genres = genres;
        }

        public string Id { get; }
        public string Artist { get; set; }
        public List<string> Genres { get; set;  }
    }
}
