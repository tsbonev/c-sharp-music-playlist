using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongPlaylistLib.Core
{
    public struct Song
    {
        public Song(string artist, string title, List<string> genres)
        {
            Id = Guid.NewGuid().ToString();
            Artist = artist;
            Title = title;
            Genres = genres ?? throw new ArgumentNullException("Genres cannot be null!");
        }

        public Song(string id, string artist, string title, List<string> genres)
        {
            Id = id;
            Artist = artist;
            Title = title;
            Genres = genres ?? throw new ArgumentNullException("Genres cannot be null!");
        }

        public string Id { get; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public List<string> Genres { get; set;  }
    }
}
