using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using NLog;
using SongPlaylistLib.Core;
using SongPlaylistREST.Helpers;

namespace SongPlaylistREST.Areas.WebService
{
    /// <summary>
    /// Summary description for SongPlaylistWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SongPlaylistWebService : System.Web.Services.WebService
    {

        /// <summary>
        /// For writing log entries.
        /// </summary>
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        private IMusicPlaylist playlist = InMemoryPlaylistProvider.GetPlaylist();

        /// <summary>
        /// Registers a song.
        /// </summary>
        /// <param name="request">The song request to register.</param>
        /// <returns>The registered song.</returns>
        public Song AddSong(RegisterSongRequest request)
        {
            Song result;
            try
            {
               var songId = playlist.Add(request);
               result = playlist.GetById(songId).GetValueOrDefault(new Song());
            }
            catch (SongAlreadyExistsException e)
            {
                result = new Song();
            }
            catch (Exception e)
            {
                logger.Error(e.ToString());
                System.Diagnostics.Trace.Write(e.ToString());
                result = new Song();
            }

            return result;
        }

        /// <summary>
        /// Retrieves a song by id.
        /// </summary>
        /// <param name="songId">The id of the song.</param>
        /// <returns>The found song.</returns>
        public Song GetSongById(string songId)
        {
            try
            {
                var possibleSong = playlist.GetById(songId);

                if (possibleSong.HasValue)
                {
                    return possibleSong.Value;
                }

                return new Song();
            }
            catch (Exception e)
            {
                logger.Error(e.ToString());
                System.Diagnostics.Trace.Write(e.ToString());
                return new Song();
            }       
        }

        /// <summary>
        /// Get all songs belonging to a certain genre.
        /// </summary>
        /// <param name="genre">The genre to query by.</param>
        /// <returns>A list of songs with matching genres.</returns>
        public List<Song> GetSongsByGenre(string genre)
        {
            try
            {
                return playlist.GetByGenre(genre);
            }
            catch (Exception e)
            {
                logger.Error(e.ToString());
                System.Diagnostics.Trace.Write(e.ToString());
                return new List<Song>();
            }
        }

        /// <summary>
        /// Get all songs belonging to a certain artist.
        /// </summary>
        /// <param name="artist">The artist to query by.</param>
        /// <returns>A list of songs of a given artist.</returns>
        public List<Song> GetSongsByArtist(string artist)
        {
            try
            {
                return playlist.GetByArtist(artist);
            }
            catch (Exception e)
            {
                logger.Error(e.ToString());
                System.Diagnostics.Trace.Write(e.ToString());
                return new List<Song>();
            }
        }
        
        /// <summary>
        /// Returns all genres.
        /// </summary>
        /// <returns>A list of all genres.</returns>
        public List<String> GetAllGenres()
        {
            try
            {
                return playlist.ViewGenres();
            }
            catch (Exception e)
            {
                logger.Error(e.ToString());
                System.Diagnostics.Trace.Write(e.ToString());
                return new List<string>();
            }
        }

        /// <summary>
        /// Retrieves all registered songs.
        /// </summary>
        /// <returns>A list of all registered songs.</returns>
        public List<Song> GetAllSongs()
        {
            try
            {
                return playlist.GetAll();
            }
            catch (Exception e)
            {
                logger.Error(e.ToString());
                System.Diagnostics.Trace.Write(e.ToString());
                return new List<Song>();
            }
        }

        /// <summary>
        /// Updates a song.
        /// </summary>
        /// <param name="songId">The id of the song to update.</param>
        /// <param name="request">The updates to enact.</param>
        /// <returns>The updated song</returns>
        public Song UpdateSong(string songId, UpdateSongRequest request)
        {
            Song result;
            try
            {
                result = playlist.Update(songId, request);
            }
            catch (SongNotFoundException e)
            {
                result = new Song();
            }
            catch (Exception e)
            {
                logger.Error(e.ToString());
                System.Diagnostics.Trace.Write(e.ToString());
                result = new Song();
            }

            return result;
        }

        /// <summary>
        /// Deletes a song by id.
        /// </summary>
        /// <param name="songId">The song id to delete.</param>
        /// <returns>The deleted song.</returns>
        public Song DeleteSong(string songId)
        {
            Song result;
            try
            {
                result = playlist.Delete(songId);
            }
            catch (SongNotFoundException e)
            {
                result = new Song();
            }
            catch (Exception e)
            {
                logger.Error(e.ToString());
                System.Diagnostics.Trace.Write(e.ToString());
                result = new Song();
            }

            return result;
        }
    }
}
