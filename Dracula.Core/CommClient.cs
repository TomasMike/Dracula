using System;
using System.Net.Sockets;

namespace Dracula.Core
{
	public class CommClient
	{
		public int port;
		public string server;

		public CommClient()
		{
			port = 13000;
			server = "127.0.0.1";
		}

		public CommClient(string server, int port)
		{
			this.port = port;
			this.server = server;
		}

		public string Call(string message)
		{
			try
			{
				// Create a TcpClient.
				// Note, for this client to work you need to have a TcpServer 
				// connected to the same address as specified by the server, port
				// combination.
				using (TcpClient client = new TcpClient(server, port))
				{
					// Translate the passed message into ASCII and store it as a Byte array.
					Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

					// Get a client stream for reading and writing.
					//  Stream stream = client.GetStream();

					NetworkStream stream = client.GetStream();

					// Send the message to the connected TcpServer. 
					stream.Write(data, 0, data.Length);


					// Receive the TcpServer.response.

					// Buffer to store the response bytes.
					data = new Byte[256];

					// String to store the response ASCII representation.
					String responseData = String.Empty;

					// Read the first batch of the TcpServer response bytes.
					Int32 bytes = stream.Read(data, 0, data.Length);
					responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);

					// Close everything.
					stream.Close();
					return responseData;
				}

			}
			catch (ArgumentNullException e)
			{
			}
			catch (SocketException e)
			{
			}
			return string.Empty;
		}
	}
}
