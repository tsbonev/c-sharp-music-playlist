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
            Genres = genres ?? throw new ArgumentNullException("Genres cannot be null!");
        }

        public Song(string id, string artist, List<string> genres)
        {
            Id = id;
            Artist = artist;
            Genres = genres ?? throw new ArgumentNullException("Genres cannot be null!");
        }

        public string Id { get; }
        public string Artist { get; set; }
        public List<string> Genres { get; set;  }
    }
}
