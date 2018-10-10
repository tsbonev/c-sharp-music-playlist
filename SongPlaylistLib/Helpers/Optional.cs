using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongPlaylistLib.Helpers
{
    /// <summary>
    /// A wrapper class that provides a null safe way to return an empty object.
    /// </summary>
    /// <typeparam name="T">The type to wrap.</typeparam>
    class Optional<T>
    {
        private Optional(){}

        private Optional(T rawValue)
        {
            this.RawValue = rawValue;
        }

        /// <summary>
        /// Creates an Optional object that contains a value.
        /// </summary>
        /// <param name="obj">The value to wrap.</param>
        /// <returns>An Optional class of the value.</returns>
        private static Optional<T> Of(T obj)
        {
            return new Optional<T>(obj);
        }

        /// <summary>
        /// Creates an Optional class that wraps null.
        /// </summary>
        /// <returns>An Optional class with a null value.</returns>
        private static Optional<T> Empty()
        {
            return new Optional<T>();
        }

        private T RawValue { get; set; }
        
        /// <summary>
        /// Returns the value of the wrapper.
        /// </summary>
        /// <returns>An object of type T.</returns>
        T Get()
        {
            return RawValue;
        }

        /// <summary>
        /// Returns true when the wrapper has a value.
        /// </summary>
        /// <returns>Whether the wrapper has a value.</returns>
        bool IsPresent()
        {
            return RawValue != null;
        }
    }
}
