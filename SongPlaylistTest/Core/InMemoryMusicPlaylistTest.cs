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
        private readonly RegisterSongRequest _registerSongRequest = new RegisterSongRequest("::artist::", "::title::", new List<string>() { "POP" });
        private readonly UpdateSongRequest _updateSongRequest = new UpdateSongRequest("::new-artist::", "::title::", new List<string>() { "POP" });

        [TestMethod]
        public void AddAndRetrieveSong()
        {
            var savedSongId = musicPlaylist.Add(_registerSongRequest);

            var savedSong = musicPlaylist.GetById(savedSongId);

            Assert.AreEqual(savedSong.HasValue, true);
            Assert.AreEqual(savedSong.Value.Artist, "::artist::");
            Assert.AreEqual(savedSong.Value.Title, "::title::");
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
        public void DeleteSong()
        {
            var savedSongId = musicPlaylist.Add(_registerSongRequest);
            var deletedSong = musicPlaylist.Delete(savedSongId);

            var missingSong = musicPlaylist.GetById(savedSongId);

            Assert.AreEqual(missingSong.HasValue, false);
            Assert.AreEqual(deletedSong.Artist, "::artist::");
            Assert.AreEqual(deletedSong.Title, "::title::");
            Assert.IsTrue(deletedSong.Genres.SequenceEqual(new List<string>() { "pop" }));
        }

        [TestMethod]
        [ExpectedException(typeof(SongNotFoundException))]
        public void DeletingNonExistingSongThrowsException()
        {
            musicPlaylist.Delete("::id::");
        }

        [TestMethod]
        [ExpectedException(typeof(SongAlreadyExistsException))]
        public void AddingSameSongThrowsException()
        {
            var savedSongId = musicPlaylist.Add(_registerSongRequest);
            musicPlaylist.Add(_registerSongRequest);
        }

        [TestMethod]
        public void UpdateSong()
        {
            var savedSongId = musicPlaylist.Add(_registerSongRequest);

            var updatedSong = musicPlaylist.Update(savedSongId, _updateSongRequest);

            var possibleSong = musicPlaylist.GetById(savedSongId);
            var retrievedSong = possibleSong.Value;

            Assert.AreEqual(retrievedSong.Artist, updatedSong.Artist);
        }

        [TestMethod]
        [ExpectedException(typeof(SongNotFoundException))]
        public void UpdatingNonExistentSongThrowsException()
        {
             musicPlaylist.Update("::id::", _updateSongRequest);
        }

        [TestMethod]
        public void GetAllSongs()
        {
            var savedSongId = musicPlaylist.Add(_registerSongRequest);
            var possibleSong = musicPlaylist.GetById(savedSongId);

            var allSongs = musicPlaylist.GetAll();

            Assert.IsTrue(allSongs.SequenceEqual(new List<Song>() { possibleSong.Value }));
        }

        [TestMethod]
        public void GetSongsByArtist()
        {
            var savedSongId = musicPlaylist.Add(_registerSongRequest);
            var possibleSong = musicPlaylist.GetById(savedSongId);

            var songList = musicPlaylist.GetByArtist(possibleSong.Value.Artist);

            Assert.IsTrue(songList.SequenceEqual(new List<Song>() { possibleSong.Value }));
        }

        [TestMethod]
        public void GetSongsByGenre()
        {
            var secondPopSongId =
                musicPlaylist.Add(new RegisterSongRequest("::new-artist::", "::title::", new List<string>() { "POP", "ROCK" }));
            var possibleSecondPopSong = musicPlaylist.GetById(secondPopSongId);

            var savedSongId = musicPlaylist.Add(_registerSongRequest);
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
            var savedSongId = musicPlaylist.Add(_registerSongRequest);

            var genres = musicPlaylist.ViewGenres();

            Assert.IsTrue(genres.SequenceEqual(_registerSongRequest.Genres.ConvertAll(s => s.ToLower())));
        }

        [TestMethod]
        public void UpdatingSongAddsItGenreToList()
        {
            var savedSongId = musicPlaylist.Add(_registerSongRequest);

            var updateRequest = new UpdateSongRequest("::artist::", "::title::", new List<string>(){"Pop", "Country"});

            musicPlaylist.Update(savedSongId, updateRequest);

            var genres = musicPlaylist.ViewGenres();

            genres.Sort();

            var expectedGenres = new List<string> { "pop", "country" };

            expectedGenres.Sort();

            Assert.IsTrue(genres.SequenceEqual(expectedGenres));
        }
    }
}
