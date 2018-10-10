using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SongPlaylistLib.Helpers;

namespace SongPlaylistLib.Core
{
    /// <summary>
    /// Provides the methods to store, update and query songs.
    /// </summary>
    interface IMusicPlaylist
    {
        /// <summary>
        /// Adds a song to the playlist.
        /// </summary>
        /// <param name="songRequest">The song request describing the song to add.</param>
        /// <returns>The added song.</returns>
        Song Add(SongRequest songRequest);

        /// <summary>
        /// Updates a song.
        /// </summary>
        /// <param name="song">The song to update.</param>
        /// <returns>The updated song.</returns>
        Song Update(Song song);

        /// <summary>
        /// Returns all songs stored.
        /// </summary>
        /// <returns>A list of all stored songs.</returns>
        List<Song> GetAll();

        /// <summary>
        /// Returns a song by id.
        /// </summary>
        /// <param name="songId">The id of the song sought.</param>
        /// <returns>An optional song.</returns>
        Optional<Song> GetById(string songId);
        
        /// <summary>
        /// Returns a list of songs by genre.
        /// </summary>
        /// <param name="genre">The genre to search by.</param>
        /// <returns>A list of song that are of the specified genre.</returns>
        List<Song> GetByGenre(Genre genre);

        /// <summary>
        /// Returns a list of songs by artist.
        /// </summary>
        /// <param name="Artist">The name of the artist.</param>
        /// <returns>A list of songs whose artist is the one specified.</returns>
        List<Song> GetByArtist(string Artist);
    }
}
