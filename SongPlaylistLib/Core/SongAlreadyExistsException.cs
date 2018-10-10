using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongPlaylistLib.Core
{
    public class SongAlreadyExistsException : Exception
    {
        public SongAlreadyExistsException()
        {
        }
    }
}
