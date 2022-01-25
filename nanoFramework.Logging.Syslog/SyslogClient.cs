using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace nanoFramework.Logging.Syslog
{
    /// <summary>
    /// Syslog Client that sends logs to a syslog server following RFC3164 format.
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
        /// Create a SyslogClient instance that will send RFC3164 compliant messages to a syslof server
        /// </summary>
        /// <param name="endpoint">Endpoint of the server</param>
        /// <param name="localHostname">'Hostname' part of the RFC3164 message</param>
        /// <param name="facility">Facility used by this logger</param>
        /// <param name="localAddress">Local IP address to bind socket (if null IPAdress.Any will be used)</param>
        /// <param name="localPort">Local port to bind socket (0 to choose available port)</param>
        public SyslogClient(IPEndPoint endpoint, string localHostname, Facility facility = default, IPAddress localAddress = null, int localPort = 0)
        {
            if (localHostname is null)
                throw new ArgumentNullException(nameof(localHostname));

            Facility = facility;
            LocalHostname = localHostname;

            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _socket.Bind(new IPEndPoint(localAddress ?? IPAddress.Any, localPort));
            _socket.Connect(endpoint);
        }

        /// <summary>
        /// Create a SyslogClient instance that will send RFC3164 compliant messages to a syslof server
        /// </summary>
        /// <param name="hostname">DNS name of the syslog server</param>
        /// <param name="port">Port of the syslog server</param>
        /// <param name="localHostname">'Hostname' part of the RFC3164 message (if null, use 'nanoframework')</param>
        /// <param name="facility">Facility used by this logger</param>
        /// <param name="localAddress">Local IP address to bind socket (if null IPAdress.Any will be used)</param>
        /// <param name="localPort">Local port to bind socket (0 to choose available port)</param>
        public SyslogClient(string hostname, int port, string localHostname, Facility facility = default, IPAddress localAddress = null, int localPort = 0)
            : this(ResolveEndPoint(hostname, port), localHostname, facility, localAddress, localPort)
        {
        }

        /// <summary>
        /// Facility used by this syslog client
        /// </summary>
        public Facility Facility { get; }
        
        /// <summary>
        /// Hostname used in the 'Hostname' field in the syslog message
        /// </summary>
        public string LocalHostname { get; }

        /// <summary>
        /// Send a message to the SysLog server
        /// </summary>
        /// <param name="severity">Severity in the message</param>
        /// <param name="messageTag">Message tag (will be followed by ': ' in the message)</param>
        /// <param name="messageContent">Message content</param>
        /// <exception cref="ObjectDisposedException"></exception>
        public void SendMessage(Severity severity, string messageTag, string messageContent) => SendMessage(Facility, severity, LocalHostname, messageTag, messageContent);

        /// <summary>
        /// Send a generic syslog message to the SysLog server
        /// </summary>
        /// <param name="facility">Facility in the message</param>
        /// <param name="severity">Severity in the message</param>
        /// <param name="hostname">Hostname in the message</param>
        /// <param name="messageTag">Message tag (will be followed by ': ' in the message)</param>
        /// <param name="messageContent">Message content</param>
        /// <exception cref="ObjectDisposedException"></exception>
        public void SendMessage(Facility facility,Severity severity, string hostname, string messageTag, string messageContent)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(SyslogClient));
            if (severity != Severity.None) // We don't send message with Severity.None which isn't a RFC6134 valid value
                _socket.Send(FormatUdpMessage(facility, severity, hostname, messageTag, messageContent));
        }

        private static byte[] FormatUdpMessage(Facility facility,Severity severity, string localHostname, string messageTag, string messageContent)
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
                headerBuilder.Append(messageTag).Append(':').Append(' ');
            headerBuilder.Append(messageContent ?? "");
            var s = headerBuilder.ToString();
            return UTF8Encoding.UTF8.GetBytes(s); // Ideally an UTF7 is required here to be RFC3164 compliant
        }

        private static IPEndPoint ResolveEndPoint(string hostname, int port)
        {
            IPHostEntry hostEntry = Dns.GetHostEntry(hostname);
            if (hostEntry is null)
                throw new ArgumentException($"Hostname `{hostname}` is not resolvable");
            return new IPEndPoint(hostEntry.AddressList[0], port);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            _disposed = true;
            ((IDisposable)_socket).Dispose();
        }
    }
}
