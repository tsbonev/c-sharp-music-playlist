using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongPlaylistLib.Core
{
    public class UpdateSongRequest
    {
        public UpdateSongRequest(string artist, string title, List<string> genres)
        {
            Artist = artist;
            Title = title;
            Genres = genres.ConvertAll(s => s.ToLower());
        }

        public string Artist { get; }
        public string Title { get; }
        public List<string> Genres { get; }
    }
}
