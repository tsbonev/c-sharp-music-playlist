using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using SongPlaylistLib.Core;
using SongPlaylistSOAP.Helpers;

namespace SongPlaylistSOAP
{
    public class SongService : ISongsService
    {

        private IMusicPlaylist playlist = InMemoryPlaylistProvider.GetPlaylist();

        public List<SongsType> GetAllSongs()
        {

            var songs = playlist.GetAll();

            var songTypeList = new List<SongsType>();

            songs.ForEach(it => songTypeList.Add(new SongsType(it.Title, it.Artist, it.Genres)));

            return songTypeList;
        }
    }
}
