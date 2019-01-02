using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SongPlaylistLib.Core;

namespace SongPlaylistSOAP.Helpers
{
    public class InMemoryPlaylistProvider
    {
        private static IMusicPlaylist playlist;

        public static IMusicPlaylist GetPlaylist()
        {
            if (playlist == null) playlist = new InMemoryMusicPlaylist();
            return playlist;
        }

    }
}