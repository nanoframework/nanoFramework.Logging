using Microsoft.Extensions.Logging;
using System;
using System.Net;

namespace nanoFramework.Logging.Syslog
{
    /// <summary>
    /// ILoggerFactory implementation that creates SyslogLogger instances. 
    /// The logger factory initializes an underlying <see cref="SyslogClient"/> that will be shared by all <see cref="SyslogLogger"/> instances it will create. 
    /// </summary>
    public class SyslogLoggerFactory : ILoggerFactory
    {
        const string DefaultHostname = "nanoframework";
        private readonly SyslogClient _client;
        private bool _disposed;

        /// <summary>
        /// Create a logging factory that will provide support for Syslog ILogger provider
        /// </summary>
        /// <param name="endpoint">Endpoint of the server</param>
        /// <param name="localHostname">'Hostname' part of the RFC3164 message</param>
        /// <param name="facility">Facility used by this logger</param>
        /// <param name="localAddress">Local IP address to bind socket (if null IPAdress.Any will be used)</param>
        /// <param name="localPort">Local port to bind socket (0 to choose available port)</param>
        public SyslogLoggerFactory(IPEndPoint endpoint, string localHostname=DefaultHostname, Facility facility = default, IPAddress localAddress = null, int localPort = 0)
        {
            _client = new SyslogClient(endpoint, localHostname, facility, localAddress, localPort);
        }

        /// <summary>
        /// Create a logging factory that will provide support for Syslog ILogger provider
        /// </summary>
        /// <param name="hostname">DNS name of the syslog server</param>
        /// <param name="port">Port of the syslog server (default to 514)</param>
        /// <param name="localHostname">'Hostname' part of the RFC3164 message</param>
        /// <param name="facility">Facility used by this logger</param>
        /// <param name="localAddress">Local IP address to bind socket (if null IPAdress.Any will be used)</param>
        /// <param name="localPort">Local port to bind socket (0 to choose available port)</param>
        public SyslogLoggerFactory(string hostname, int port= SyslogClient.DefaultPort, string localHostname=DefaultHostname, Facility facility = default, IPAddress localAddress = null, int localPort = 0) 
        {
            _client = new SyslogClient(hostname, port, localHostname, facility , localAddress, localPort);
        }

        /// <inheritdoc/>
        public ILogger CreateLogger(string categoryName)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(SyslogLoggerFactory));
            return new SyslogLogger(_client,categoryName);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            _disposed = true;
            // If logger created through this factory are used after this you will get exception
            _client.Dispose();
        }
    }
}
