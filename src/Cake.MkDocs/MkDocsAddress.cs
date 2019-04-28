using System;

namespace Cake.MkDocs
{
    /// <summary>
    /// Represents <c>IP</c> address and port used in settings.
    /// </summary>
    public class MkDocsAddress
    {
        /// <summary>
        /// Gets address <c>IP</c>/host.
        /// </summary>
        /// <value><c>localhost</c> - default.</value>
        public string Ip { get; }

        /// <summary>
        /// Gets address port.
        /// </summary>
        /// <value><c>8000</c> - default.</value>
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
        /// <param name="ip">The address Host/<c>IP</c>.</param>
        /// <param name="port">The address port (from <c>1</c> to <c>65535</c>).</param>
        /// <exception cref="ArgumentNullException">Thrown when ip parameter is empty or white space.</exception>
        /// <exception cref="ArgumentException">Thrown when ip parameter is unknown <c>IP</c> or Host. See <see cref="Uri.CheckHostName(string)"/> validation.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when port parameter is out of <c>1</c> to <c>65535</c> range.</exception>
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
        /// <returns><c>IP</c> address in format: <c>IP:PORT</c>.</returns>
        public override string ToString() => $"{Ip}:{Port}";
    }
}