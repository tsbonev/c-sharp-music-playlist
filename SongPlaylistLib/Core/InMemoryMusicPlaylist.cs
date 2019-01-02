using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongPlaylistLib.Core
{
    public class InMemoryMusicPlaylist : IMusicPlaylist
    {
        private Dictionary<string, Song> Songs { get; set; }

        public List<string> Genres { get; set; }

        public InMemoryMusicPlaylist()
        {
            Songs = new Dictionary<string, Song>();
            Genres = new List<string>();
        }

        public string Add(RegisterSongRequest registerSongRequest)
        {
            var song = new Song(registerSongRequest.Artist, registerSongRequest.Title, registerSongRequest.Genres);

            if (SongExists(song)) throw new SongAlreadyExistsException();

            registerSongRequest.Genres.ForEach(genre => AddGenre(genre));

            Songs.Add(song.Id, song);

            return song.Id;
        }

        public Song Update(string id, UpdateSongRequest request)
        {
            try
            {
                var songToUpdate = Songs[id];

                request.Genres.ForEach(genre => AddGenre(genre));
                var updatedSong = new Song(id, request.Artist, request.Title, request.Genres);
                Songs[id] = updatedSong;
                return updatedSong;
            }
            catch (KeyNotFoundException e)
            {
                throw new SongNotFoundException();
            }
        }

        public Song Delete(string id)
        {
            try
            {
                var songToDelete = Songs[id];
                Songs.Remove(id);
                return songToDelete;
            }
            catch (KeyNotFoundException e)
            {
                throw new SongNotFoundException();
            }
        }

        public List<Song> GetAll()
        {
            return Songs.Values.ToList();
        }

        public Song? GetById(string songId)
        {
            if (songId == null) return null;

            try
            {
                return Songs[songId];
            }
            catch(KeyNotFoundException e)
            {
                return null;
            }
        }

        public List<Song> GetByGenre(string genre)
        {
            return Songs.Values.Where(s => s.Genres.Contains(genre.ToLower())).ToList();
        }

        public List<Song> GetByArtist(string artist)
        {
            return Songs.Values.Where(s => s.Artist == artist).ToList();
        }

        public List<string> ViewGenres()
        {
            return Genres;
        }

        private void AddGenre(string genre)
        {
            if (!Genres.Contains(genre.ToLower())) Genres.Add(genre.ToLower());
        }

        private bool SongExists(Song song)
        {
            foreach (var songsValue in Songs.Values)
            {
                if (songsValue.Artist == song.Artist
                    && songsValue.Title == song.Title
                    && songsValue.Genres.SequenceEqual(song.Genres))
                    return true;
            }

            return false;
        }
    }
}
