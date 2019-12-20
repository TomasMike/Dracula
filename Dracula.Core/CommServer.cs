using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dracula.Core
{


	public class CommServer
	{
		public Func<object, object> OnCall;
		public Action<string> Log = (s)=> { };
		public Encoding EncodingToUse;
		public TcpListener server;
		public void Start()
		{
			if (EncodingToUse == null)
				EncodingToUse = Encoding.ASCII;

			server = null;
			try
			{
				// Set the TcpListener on port 13000.
				Int32 port = 13000;
				IPAddress localAddr = IPAddress.Parse("127.0.0.1");



				// TcpListener server = new TcpListener(port);
				server = new TcpListener(localAddr, port);

				// Start listening for client requests.
				server.Start();

				// Buffer for reading data
				Byte[] bytes = new Byte[256];
				String data = null;

				// Enter the listening loop.
				Timer t = new Timer((o) => 
				{
					try
					{
						// Perform a blocking call to accept requests.
						// You could also user server.AcceptSocket() here.
						TcpClient client = server.AcceptTcpClient();

						data = null;

						Log("trying to get stream");
						// Get a stream object for reading and writing
						NetworkStream stream = client.GetStream();
						int i;
						// Loop to receive all the data sent by the client.
						while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
						{
							// Translate data bytes to a ASCII string.

							data = EncodingToUse.GetString(bytes, 0, i);
							var retVal = CallOnCall($"ServerReceived: {data}");

							byte[] msg = EncodingToUse.GetBytes(retVal);

							// Send back a response.
							stream.Write(msg, 0, msg.Length);
						}

						// Shutdown and end connection
						client.Close();
					}
					catch (Exception)
					{

					}
				},null,0,2000);

			
			}
			catch (SocketException e)
			{
				Console.WriteLine("SocketException: {0}", e);
			}
			finally
			{
				// Stop listening for new clients.
				server.Stop();
			}
		}

		private string CallOnCall(string message)
		{
			string retVal = string.Empty;
			if (OnCall != null)
				 retVal = OnCall(message).ToString();
			return retVal;

		}

	}
}
