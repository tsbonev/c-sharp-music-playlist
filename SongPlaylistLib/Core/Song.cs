using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongPlaylistLib.Core
{
    class Song
    {
        public string Id { get; }
        public string Artist { get; set; }
        public List<Genre> Genres { get; set; }
    }
}
