using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SongPlaylistLib.Helpers;

namespace SongPlaylistTest
{
    [TestClass]
    public class OptionalTest
    {
        [TestMethod]
        public void WrapValueInOptional()
        {
            var data = "::data::";

            var optionalData = Optional<String>.Of(data);

            Assert.AreEqual(optionalData.IsPresent(), true);
            Assert.AreEqual(optionalData.Get(), data);
        }

        [TestMethod]
        public void WrapNullInOptional()
        {
            var optionalData = Optional<String>.Empty();
            Assert.AreEqual(optionalData.IsPresent(), false);
            Assert.AreEqual(optionalData.Get(), null);
        }
    }
}
