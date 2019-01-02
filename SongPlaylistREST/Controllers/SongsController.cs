using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using NLog;
using SongPlaylistLib.Core;
using SongPlaylistREST.Helpers;

namespace SongPlaylistREST.Controllers
{
    public class SongsController : ApiController
    {

        private IMusicPlaylist playlist = InMemoryPlaylistProvider.GetPlaylist();
        private Logger logger = NLog.LogManager.GetCurrentClassLogger();

        [HttpGet]
        [Route("api/songs")]
        public HttpResponseMessage GetAllSongs()
        {
            logger.Info("Retrieving all songs");

            return this.Request.CreateResponse(HttpStatusCode.OK, playlist.GetAll());
        }

        [HttpGet]
        [Route("api/songs/{id}")]
        public HttpResponseMessage GetSongById(string id)
        {
            logger.Info("Retrieving song with id " + id);

            var possibleSong = playlist.GetById(id);

            if (possibleSong.HasValue)
            {
                logger.Info("Song found by id");
                return this.Request.CreateResponse(HttpStatusCode.OK, playlist.GetById(id));
            }
            else
            {
                logger.Warn("Song not found by id");
                return this.Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [HttpPost]
        [Route("api/songs")]
        public HttpResponseMessage RegisterSong([FromBody]RegisterSongRequest request)
        {
            try
            {
                logger.Info("Registering song");
                var savedSong = playlist.Add(request);
                return this.Request.CreateResponse(HttpStatusCode.Created, savedSong);
            }
            catch (SongAlreadyExistsException e)
            {
                logger.Warn("Song already registered");
                return this.Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpPut]
        [Route("api/songs/{id}")]
        public HttpResponseMessage UpdateSong(string id, [FromBody]UpdateSongRequest request)
        {
            logger.Info("Updating song");
            try
            {
                var updatedSong = playlist.Update(id, request);
                return this.Request.CreateResponse(HttpStatusCode.OK, updatedSong);
            }
            catch (SongNotFoundException e)
            {
                logger.Warn("Could not find song to update");
                return this.Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [HttpDelete]
        [Route("api/songs/{id}")]
        public HttpResponseMessage DeleteSongById(string id)
        {
            try
            {
                logger.Info("Deleting song with id " + id);
                var deletedSong = playlist.Delete(id);
                return this.Request.CreateResponse(HttpStatusCode.NoContent, deletedSong);
            }
            catch (SongNotFoundException e)
            {
                logger.Info("Song could not be found");
                return this.Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }
    }
}
