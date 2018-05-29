using System;

namespace Cake.MkDocs
{
    /// <summary>
    /// Represents IP address and port used in settings.
    /// </summary>
    public class MkDocsAddress
    {
        /// <summary>
        /// Gets address IP/host
        /// <value><c>localhost</c> - default</value>
        /// </summary>
        public string Ip { get; }

        /// <summary>
        /// Gets address port
        /// <value><c>8000</c> - default</value>
        /// </summary>
        public int Port { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MkDocsAddress"/> class.
        /// </summary>
        public MkDocsAddress()
        {
            Ip = "default";
            Port = 8000;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MkDocsAddress"/> class.
        /// </summary>
        /// <param name="ip">The address host/ip.</param>
        /// <param name="port">The address port (from <c>1</c> to <c>65535</c>)</param>
        public MkDocsAddress(string ip, int port)
        {
            if (string.IsNullOrWhiteSpace(ip))
            {
                throw new ArgumentNullException(nameof(ip));
            }
            if (Uri.CheckHostName(ip) == UriHostNameType.Unknown)
            {
                throw new ArgumentException("Unknown IP/Host", nameof(ip));
            }

            if (port < 1 || port > 65535)
            {
                throw new ArgumentOutOfRangeException(nameof(port));
            }

            Ip = ip;
            Port = port;
        }

        /// <summary>
        /// Overrides default string representation.
        /// </summary>
        /// <returns>IP address in format: IP:PORT</returns>
        public override string ToString() => $"{Ip}:{Port}";
    }
}