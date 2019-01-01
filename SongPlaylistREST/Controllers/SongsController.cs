using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using SongPlaylistLib.Core;

namespace SongPlaylistREST.Controllers
{
    public class SongsController : ApiController
    {

        private IMusicPlaylist playlist = new InMemoryMusicPlaylist();

        // GET api/songs
        public string Get()
        {
            return JsonConvert.SerializeObject(playlist.GetAll());
        }

        // GET api/songs/{id}
        public string Get(string id)
        {
            return JsonConvert.SerializeObject(playlist.GetById(id));
        }

        // POST api/values
        public string Post([FromBody]string request)
        {
            var savedSong = playlist.Add(JsonConvert.DeserializeObject<SongRequest>(request));
            return savedSong;
        }

        // PUT api/values/5
        public string Put(int id, [FromBody]string value)
        {
            return "value";
        }

        // DELETE api/values/5
        public string Delete(int id)
        {
            return "value";
        }
    }
}
