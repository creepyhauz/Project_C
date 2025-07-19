using System.Net.Sockets;
using System.Text;

namespace ProjectC_Server
{
    public class Client
    {
        private readonly TcpClient client;

        public Client(TcpClient client)
        {
            this.client = client;
        }

        public async Task HandleClientAsync()
        {
            using var stream = client.GetStream();
            var buffer = new byte[1024];

            try
            {
                while (true)
                {
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                        break; // Клиент отключился

                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"[Client] {message}");

                    // Эхо-ответ
                    byte[] response = Encoding.UTF8.GetBytes("Принято\n");
                    await stream.WriteAsync(response, 0, response.Length);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Ошибка ввода-вывода: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка клиента: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Клиент отключен.");
                client.Close();
                client.Dispose();
            }
        }
    }
}