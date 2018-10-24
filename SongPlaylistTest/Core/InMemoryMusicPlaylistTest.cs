using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SongPlaylistLib.Core;

namespace SongPlaylistTest.Core
{
    [TestClass]
    public class InMemoryMusicPlaylistTest
    {
        private readonly IMusicPlaylist musicPlaylist = new InMemoryMusicPlaylist();
        private readonly SongRequest songRequest = new SongRequest("::artist::", new List<string>() { "POP" });


        [TestMethod]
        public void AddAndRetrieveSong()
        {
            var savedSongId = musicPlaylist.Add(songRequest);

            var savedSong = musicPlaylist.GetById(savedSongId);

            Assert.AreEqual(savedSong.HasValue, true);
            Assert.AreEqual(savedSong.Value.Artist, "::artist::");
            Assert.IsTrue(savedSong.Value.Genres.SequenceEqual(new List<string>() { "pop" }));
        }

        [TestMethod]
        public void RetrievingNonExistingSongReturnsNull()
        {
            var nullSong = musicPlaylist.GetById("::non-existent-id::");

            Assert.AreEqual(nullSong.HasValue, false);
        }

        [TestMethod]
        public void RetrievingSongWithNullForIdReturnsNull()
        {
            var nullSong = musicPlaylist.GetById(null);

            Assert.AreEqual(nullSong.HasValue, false);
        }

        [TestMethod]
        [ExpectedException(typeof(SongAlreadyExistsException))]
        public void AddingSameSongThrowsException()
        {
            var savedSongId = musicPlaylist.Add(songRequest);
            musicPlaylist.Add(songRequest);
        }

        [TestMethod]
        public void UpdateSong()
        {
            var savedSongId = musicPlaylist.Add(songRequest);
            var possibleSong = musicPlaylist.GetById(savedSongId);
            var retrievedSong = possibleSong.Value;

            retrievedSong.Artist = "::new-artist::";

            var updatedSong = musicPlaylist.Update(retrievedSong);

            Assert.AreEqual(retrievedSong, updatedSong);
        }

        [TestMethod]
        public void UpdatingNonExistentSongUpserts()
        {
            var song = new Song("::artist::", new List<string> { "POP" });

            var upsertedSong = musicPlaylist.Update(song);

            Assert.AreEqual(song.Artist, upsertedSong.Artist);
            Assert.IsTrue(song.Genres.SequenceEqual(upsertedSong.Genres));
        }

        [TestMethod]
        public void GetAllSongs()
        {
            var savedSongId = musicPlaylist.Add(songRequest);
            var possibleSong = musicPlaylist.GetById(savedSongId);

            var allSongs = musicPlaylist.GetAll();

            Assert.IsTrue(allSongs.SequenceEqual(new List<Song>() { possibleSong.Value }));
        }

        [TestMethod]
        public void GetSongsByArtist()
        {
            var savedSongId = musicPlaylist.Add(songRequest);
            var possibleSong = musicPlaylist.GetById(savedSongId);

            var songList = musicPlaylist.GetByArtist(possibleSong.Value.Artist);

            Assert.IsTrue(songList.SequenceEqual(new List<Song>() { possibleSong.Value }));
        }

        [TestMethod]
        public void GetSongsByGenre()
        {
            var secondPopSongId =
                musicPlaylist.Add(new SongRequest("::new-artist::", new List<string>() { "POP", "ROCK" }));
            var possibleSecondPopSong = musicPlaylist.GetById(secondPopSongId);

            var savedSongId = musicPlaylist.Add(songRequest);
            var possibleSong = musicPlaylist.GetById(savedSongId);

            var songList = musicPlaylist.GetByGenre("pop");

            var expectedList = new List<Song>() { possibleSecondPopSong.Value, possibleSong.Value };

            Assert.IsTrue(songList.Contains(expectedList[0]));
            Assert.IsTrue(songList.Contains(expectedList[1]));
        }

        [TestMethod]
        public void GenresAreCaseInsensitive()
        {
            var lowercaseList = musicPlaylist.GetByGenre("pop");
            var randomcaseList = musicPlaylist.GetByGenre("pOp");
            var uppercaseList = musicPlaylist.GetByGenre("POP");

            Assert.IsTrue(lowercaseList.SequenceEqual(randomcaseList));
            Assert.IsTrue(lowercaseList.SequenceEqual(uppercaseList));
        }

        [TestMethod]
        public void AddingSongAddsItGenreToList()
        {
            var savedSongId = musicPlaylist.Add(songRequest);

            var genres = musicPlaylist.ViewGenres();

            Assert.IsTrue(genres.SequenceEqual(songRequest.Genres.ConvertAll(s => s.ToLower())));
        }

        [TestMethod]
        public void UpdatingSongAddsItGenreToList()
        {
            var savedSongId = musicPlaylist.Add(songRequest);

            var updatedSong = musicPlaylist.Update(new Song(savedSongId, "::artist::", new List<string> { "Country", "pop" }));

            var genres = musicPlaylist.ViewGenres();

            genres.Sort();

            var expectedGenres = new List<string> { "pop", "country" };

            expectedGenres.Sort();

            Assert.IsTrue(genres.SequenceEqual(expectedGenres));
        }
    }
}
