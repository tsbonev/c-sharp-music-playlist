using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SongPlaylistSOAP
{
    [ServiceContract]
    public interface ISongsService
    {
        [OperationContract]
        List<SongsType> GetAllSongs();
    }


    [DataContract]
    public class SongsType
    {
        public SongsType(string artist, string title, List<string> genres)
        {
            this.genres = genres;
            this.artist = artist;
            this.title = title;
        }

        List<string> genres;
        string title;
        string artist;

        [DataMember]
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        [DataMember]
        public string Artist
        {
            get { return artist; }
            set { artist = value; }
        }

        [DataMember]
        public List<string> Genres
        {
            get { return genres; }
            set { genres = value; }
        }
    }
}
