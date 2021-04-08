//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

using Microsoft.Extensions.Logging;

namespace nanoFramework.Logging.Debug
{
    /// <summary>
    /// Provides a simple Debugger logger
    /// </summary>
    public class DebugLoggerFactory : ILoggerFactory
    {
        /// <inheritdoc/>
        public ILogger CreateLogger(string categoryName)
        {
            return new DebugLogger(categoryName);
        }

        /// <inheritdoc />
        public void Dispose()
        {
        }
    }
}