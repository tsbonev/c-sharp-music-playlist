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

        public string Add(SongRequest songRequest)
        {
            var song = new Song(songRequest.Artist, songRequest.Genres);

            if (SongExists(song)) throw new SongAlreadyExistsException();

            songRequest.Genres.ForEach(genre => AddGenre(genre));

            Songs.Add(song.Id, song);

            return song.Id;
        }

        public Song Update(Song song)
        {
            song.Genres.ForEach(genre => AddGenre(genre));
            Songs[song.Id] = song;
            return song;
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
                    && songsValue.Genres.SequenceEqual(song.Genres))
                    return true;
            }

            return false;
        }
    }
}
