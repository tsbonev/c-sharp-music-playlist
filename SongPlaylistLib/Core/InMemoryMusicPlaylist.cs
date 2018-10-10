using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SongPlaylistLib.Helpers;

namespace SongPlaylistLib.Core
{
    public class InMemoryMusicPlaylist : IMusicPlaylist
    {
        public InMemoryMusicPlaylist()
        {
            Songs = new Dictionary<string, Song>();
        }

        private Dictionary<string, Song> Songs { get; set; }

        public string Add(SongRequest songRequest)
        {
            var song = new Song(songRequest.Artist, songRequest.Genres);

            if (SongExists(song)) throw new SongAlreadyExistsException();

            Songs.Add(song.Id, song);

            return song.Id;
        }

        public Song Update(Song song)
        {
            Songs[song.Id] = song;
            return song;
        }

        public List<Song> GetAll()
        {
            return Songs.Values.ToList();
        }

        public Optional<Song> GetById(string songId)
        {
            var possibleSong = Songs[songId];

            return possibleSong == null ? Optional<Song>.Empty() : Optional<Song>.Of(possibleSong);
        }

        public List<Song> GetByGenre(Genre genre)
        {
            return Songs.Values.Where(s => s.Genres.Contains(genre)).ToList();
        }

        public List<Song> GetByArtist(string artist)
        {
            return Songs.Values.Where(s => s.Artist == artist).ToList();
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
