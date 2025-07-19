using System.Net;
using System.Net.Sockets;

namespace ProjectC_Server
{
    internal class Server
    {
        TcpListener listener;
        Thread ServerThread;

        public bool IsRunning => listener.Server.IsBound;
        public Server()
        {
            // Initialize the server
            Console.WriteLine("Starting server...");
            listener = new(IPAddress.Any, 5467);
            listener.Start();

            ServerThread = new Thread(ServerLoop);
            ServerThread.IsBackground = true;
            ServerThread.Start();
            Console.WriteLine("Server started on port 5467. Waiting for clients...");
        }

        private async void ServerLoop()
        {
            while (IsRunning)
            {
                try
                {
                    // Accept a new client connection
                    TcpClient tcpClient = listener.AcceptTcpClient();
                    Console.WriteLine("Client connected: " + tcpClient.Client.RemoteEndPoint);
                   
                    Client client = new Client(tcpClient);
                    // Handle the client connection asynchronously
                    await client.HandleClientAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error accepting client: " + ex.Message);
                }
            }
        }
    }
}