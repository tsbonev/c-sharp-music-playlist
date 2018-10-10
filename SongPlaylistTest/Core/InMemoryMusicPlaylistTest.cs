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
        private readonly SongRequest songRequest = new SongRequest("::artist::", new List<Genre>() { Genre.POP });
        

        [TestMethod]
        public void AddAndRetrieveSong()
        {
            var savedSongId = musicPlaylist.Add(songRequest);

            var savedSong = musicPlaylist.GetById(savedSongId);

            Assert.AreEqual(savedSong.IsPresent(), true);
            Assert.AreEqual(savedSong.Get().Artist, "::artist::");
            Assert.IsTrue(savedSong.Get().Genres.SequenceEqual(new List<Genre>() {Genre.POP}));
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
            var possibleSong= musicPlaylist.GetById(savedSongId);
            var retrievedSong = possibleSong.Get();

            retrievedSong.Artist = "::new-artist::";

            var updatedSong = musicPlaylist.Update(retrievedSong);

            Assert.AreEqual(retrievedSong, updatedSong);
        }

        [TestMethod]
        public void GetAllSongs()
        {
            var savedSongId = musicPlaylist.Add(songRequest);
            var possibleSong = musicPlaylist.GetById(savedSongId);

            var allSongs = musicPlaylist.GetAll();

            Assert.IsTrue(allSongs.SequenceEqual(new List<Song>(){possibleSong.Get()}));
        }

        [TestMethod]
        public void GetSongsByArtist()
        {
            var savedSongId = musicPlaylist.Add(songRequest);
            var possibleSong = musicPlaylist.GetById(savedSongId);

            var songList = musicPlaylist.GetByArtist(possibleSong.Get().Artist);

            Assert.IsTrue(songList.SequenceEqual(new List<Song>() { possibleSong.Get() }));
        }

        [TestMethod]
        public void GetSongsByGenre()
        {
            var secondPopSongId =
                musicPlaylist.Add(new SongRequest("::new-artist::", new List<Genre>() {Genre.POP, Genre.ROCK}));
            var possibleSecondPopSong = musicPlaylist.GetById(secondPopSongId);

            var savedSongId = musicPlaylist.Add(songRequest);
            var possibleSong = musicPlaylist.GetById(savedSongId);

            var songList = musicPlaylist.GetByGenre(Genre.POP);

            var expectedList = new List<Song>() {possibleSecondPopSong.Get(), possibleSong.Get()};

            Assert.IsTrue(songList.Contains(expectedList[0]));
            Assert.IsTrue(songList.Contains(expectedList[1]));
        }
    }
}
