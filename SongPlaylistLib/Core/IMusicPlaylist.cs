using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongPlaylistLib.Core
{
    /// <summary>
    /// Provides the methods to store, update and query songs.
    /// </summary>
    public interface IMusicPlaylist
    {
        /// <summary>
        /// Adds a song to the playlist.
        /// </summary>
        /// <param name="registerSongRequest">The song request describing the song to add.</param>
        /// <exception cref="SongPlaylistLib.Core.SongAlreadyExistsException">Thrown when the song
        /// that is being added already exists.</exception>
        /// <returns>The id of the added song.</returns>
        string Add(RegisterSongRequest registerSongRequest);

        /// <summary>
        /// Updates a song.
        /// </summary>
        /// <param name="id">The id of the song to update.</param>
        /// <param name="request">The requested song update.</param>
        /// <exception cref="SongNotFoundException">Thrown when a song by the passed id is not found.</exception>
        /// <returns>The updated song.</returns>
        Song Update(string id, UpdateSongRequest request);

        /// <summary>
        /// Deletes a song by id.
        /// </summary>
        /// <param name="id">The id of the song to delete.</param>
        /// <exception cref="SongPlaylistLib.Core.SongNotFoundException">Thrown when the song is not found.</exception>
        /// <returns></returns>
        Song Delete(string id);

        /// <summary>
        /// Returns all songs stored.
        /// </summary>
        /// <returns>A list of all stored songs.</returns>
        List<Song> GetAll();

        /// <summary>
        /// Returns a song by id.
        /// </summary>
        /// <param name="songId">The id of the song sought.</param>
        /// <returns>A nullable song.</returns>
        Song? GetById(string songId);
        
        /// <summary>
        /// Returns a list of songs by genre.
        /// </summary>
        /// <param name="genre">The genre to search by.</param>
        /// <returns>A list of song that are of the specified genre.</returns>
        List<Song> GetByGenre(string genre);

        /// <summary>
        /// Returns a list of songs by artist.
        /// </summary>
        /// <param name="Artist">The name of the artist.</param>
        /// <returns>A list of songs whose artist is the one specified.</returns>
        List<Song> GetByArtist(string Artist);

        /// <summary>
        /// Returns the list of stored genres.
        /// </summary>
        /// <returns>A list of all stored genres.</returns>
        List<string> ViewGenres();
    }
}
