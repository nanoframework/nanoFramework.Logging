//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//
using Microsoft.Extensions.Logging;
using System.IO;

namespace nanoFramework.Logging.Stream
{
    /// <summary>
    /// Provides a  simple Stream logger
    /// </summary>
    public class StreamLoggerFactory : ILoggerFactory
    {
        private System.IO.Stream _stream;

        /// <summary>
        /// Create a new instance of Stream Logger Factory from a stream
        /// </summary>
        /// <param name="stream">The stream</param>
        public StreamLoggerFactory(System.IO.Stream stream)
        {
            _stream = stream;
        }

        /// <summary>
        /// Create a new instance of Stream Logger Factory from a file
        /// </summary>
        /// <param name="fileName"></param>
        public StreamLoggerFactory(string fileName)
        {
            _stream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);
        }

        /// <inheritdoc/>
        public ILogger CreateLogger(string categoryName)
        {
            if ((!_stream.CanSeek) || (!_stream.CanWrite))
            {
                throw new IOException();
            }

            return new StreamLogger(_stream);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _stream?.Dispose();
        }
    }
}