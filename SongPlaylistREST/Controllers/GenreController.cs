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
    public class GenreController : ApiController
    {
        private IMusicPlaylist playlist = InMemoryPlaylistProvider.GetPlaylist();
        private Logger logger = NLog.LogManager.GetCurrentClassLogger();

        [HttpGet]
        [Route("api/genres/{genre}")]
        public HttpResponseMessage GetByGenre(string genre)
        {
            var songsByGenre = playlist.GetByGenre(genre);

            logger.Info("Retrieving list of songs of genre " + genre);

            return this.Request.CreateResponse(HttpStatusCode.OK, songsByGenre);
        }

        [HttpGet]
        [Route("api/genres")]
        public HttpResponseMessage GetAllGenres()
        {
            logger.Info("Retrieving list of all genres");
            return this.Request.CreateResponse(HttpStatusCode.OK, playlist.ViewGenres());
        }
    }
}