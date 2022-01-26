//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace nanoFramework.Logging.Syslog
{
    /// <summary>
    /// Syslog Client that sends logs to a Syslog server following RFC3164 format.
    /// </summary>
    public class SyslogClient : IDisposable
    {
        private readonly Socket _socket;
        private bool _disposed;

        /// <summary>
        /// Default Syslog UDP port : 514
        /// </summary>
        public const int DefaultPort = 514;

        /// <summary>
        /// Create a <see cref="SyslogClient"/> instance that will send RFC3164 compliant messages to a Syslog server.
        /// </summary>
        /// <param name="endpoint">Endpoint of the server.</param>
        /// <param name="localHostname">'Hostname' part of the RFC3164 message.</param>
        /// <param name="facility"><see cref="Facility"/> used by this logger.</param>
        /// <param name="localAddress">Local IP address to bind <see cref="Socket"/> (if null <see cref="IPAddress.Any"/> will be used).</param>
        /// <param name="localPort">Local port to bind <see cref="Socket"/> to (0 to choose available port).</param>
        public SyslogClient(
            IPEndPoint endpoint,
            string localHostname,
            Facility facility = default,
            IPAddress localAddress = null,
            int localPort = 0)
        {
            Facility = facility;
            LocalHostname = localHostname ?? throw new ArgumentNullException(nameof(localHostname));

            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _socket.Bind(new IPEndPoint(localAddress ?? IPAddress.Any, localPort));
            _socket.Connect(endpoint);
        }

        /// <summary>
        /// Create a <see cref="SyslogClient"/> instance that will send RFC3164 compliant messages to a Syslog server.
        /// </summary>
        /// <param name="hostname">Fully Qualified Domain Name of the Syslog server.</param>
        /// <param name="port">Port of the Syslog server.</param>
        /// <param name="localHostname">'Hostname' part of the RFC3164 message (if null, 'nanoframework' will be used).</param>
        /// <param name="facility"><see cref="Facility"/> used by this logger.</param>
        /// <param name="localAddress">Local IP address to bind <see cref="Socket"/> to (if null <see cref="IPAddress.Any"/> will be used).</param>
        /// <param name="localPort">Local port to bind <see cref="Socket"/> to (0 to choose available port).</param>
        public SyslogClient(
            string hostname,
            int port,
            string localHostname,
            Facility facility = default,
            IPAddress localAddress = null,
            int localPort = 0)
            : this(
                  ResolveEndPoint(hostname, port),
                  localHostname,
                  facility,
                  localAddress,
                  localPort)
        {
        }

        /// <summary>
        /// Facility used by this Syslog client.
        /// </summary>
        public Facility Facility { get; }

        /// <summary>
        /// Hostname used in the 'Hostname' field in the Syslog message.
        /// </summary>
        public string LocalHostname { get; }

        /// <summary>
        /// Send a message to a SysLog server.
        /// </summary>
        /// <param name="severity"><see cref="Severity"/> of the message.</param>
        /// <param name="tag">Message tag (will be followed by ': ' in the message).</param>
        /// <param name="content">Message content.</param>
        /// <exception cref="ObjectDisposedException"></exception>
        public void SendMessage(
            Severity severity,
            string tag,
            string content) => SendMessage(
                Facility,
                severity,
                LocalHostname,
                tag,
                content);

        /// <summary>
        /// Send a generic Syslog message to a SysLog server.
        /// </summary>
        /// <param name="facility"><see cref="Facility"/> in the message</param>
        /// <param name="severity"><see cref="Severity"/> in the message</param>
        /// <param name="hostname">Hostname in the message</param>
        /// <param name="tag">Message tag (will be followed by ': ' in the message).</param>
        /// <param name="content">Message content</param>
        /// <exception cref="ObjectDisposedException"></exception>
        public void SendMessage(
            Facility facility,
            Severity severity,
            string hostname,
            string tag,
            string content)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(SyslogClient));
            }

            // We don't send message with Severity.None which isn't a RFC6134 valid value
            if (severity != Severity.None)
            {
                _socket.Send(FormatUdpMessage(facility, severity, hostname, tag, content));
            }
        }

        private static byte[] FormatUdpMessage(
            Facility facility,
            Severity severity,
            string localHostname,
            string messageTag,
            string messageContent)
        {
            int priorityValue = ((int)facility * 8) + (int)severity;

            var headerBuilder = new StringBuilder();

            // RFC3164 PRI
            headerBuilder.Append('<').Append(priorityValue).Append('>');

            // RFC3164 HEADER
            string timestamp = DateTime.UtcNow.ToString("MMM dd HH:mm:ss");
            headerBuilder.Append(timestamp).Append(' ');
            headerBuilder.Append(localHostname).Append(' ');

            // RFC3164 MSG
            if (messageTag != null)
            {
                headerBuilder.Append(messageTag).Append(':').Append(' ');
            }

            headerBuilder.Append(messageContent ?? "");
            var s = headerBuilder.ToString();
            return UTF8Encoding.UTF8.GetBytes(s); // Ideally an UTF7 is required here to be RFC3164 compliant
        }

        private static IPEndPoint ResolveEndPoint(
            string hostname,
            int port)
        {
            IPHostEntry hostEntry = Dns.GetHostEntry(hostname);
            if (hostEntry is null)
            {
                throw new ArgumentException($"Hostname `{hostname}` is not resolvable");
            }

            return new IPEndPoint(hostEntry.AddressList[0], port);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            _disposed = true;
            ((IDisposable)_socket)?.Dispose();
        }
    }
}
