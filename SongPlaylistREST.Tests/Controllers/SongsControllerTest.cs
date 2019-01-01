using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SongPlaylistLib.Core;
using SongPlaylistREST;
using SongPlaylistREST.Controllers;

namespace SongPlaylistREST.Tests.Controllers
{
    [TestClass]
    public class SongsControllerTest
    {
        [TestMethod]
        public void Get()
        {
            SongsController controller = new SongsController();

            var songId = controller.Post(JsonConvert.SerializeObject(
                new SongRequest("::artist::", new List<string>(){":genre-1::", "::genre-2::"})));

            var result = controller.Get();

            Assert.IsNotNull(result);
            Assert.AreEqual(result, JsonConvert.SerializeObject(new List<Song>(){new Song(songId, "::artist::", new List<string>(){ ":genre-1::", "::genre-2::" } )}));
        }
    }
}
