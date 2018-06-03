using System.Net.Sockets;
using System.Threading;
using Cake.Core;

public static class Network
{
	private static IConsole _console = new CakeConsole();

	private static bool TryPing(string hostUri, int portNumber)
	{
		try
		{
			using (var client = new TcpClient(hostUri, portNumber))
			{
				return true;
			}
		}
		catch (SocketException)
		{
			return false;
		}
	}

	public static bool PingHost(string hostUri, int portNumber)
	{
		var result = false;
		for (int i = 0; i < 10; i++)
		{
			result = TryPing(hostUri, portNumber);
			if (!result)
			{
				Thread.Sleep(1000);
			}
			else
			{
				break;
			}
		}
		
		return result;
	}
}
