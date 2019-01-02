using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NLog;
using SongPlaylistLib.Core;
using SongPlaylistREST.Helpers;

namespace SongPlaylistREST.Controllers
{
    public class ArtistController : ApiController
    {
        private IMusicPlaylist playlist = InMemoryPlaylistProvider.GetPlaylist();

        private Logger logger = NLog.LogManager.GetCurrentClassLogger();

        [HttpGet]
        [Route("api/artists")]
        public HttpResponseMessage GetByArtist([FromUri] string artist)
        {
            var allArtists = playlist.GetByArtist(artist);

            logger.Info("Retrieving all artists");

            return this.Request.CreateResponse(HttpStatusCode.OK, allArtists);
        }
    }
}
