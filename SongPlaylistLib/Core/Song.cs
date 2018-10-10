using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongPlaylistLib.Core
{
    public class Song
    {
        public Song(string artist, List<Genre> genres)
        {
            this.Id = System.Guid.NewGuid().ToString();
            this.Artist = artist;
            this.Genres = genres;
        }

        public string Id { get; }
        public string Artist { get; set; }
        public List<Genre> Genres { get; set; }
    }
}
