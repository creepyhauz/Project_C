using System;
using System.Net.Sockets;
using System.Text;

namespace ProjectC_Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using TcpClient client = new TcpClient("127.0.0.1", 5467); // IP и порт сервера
                using NetworkStream stream = client.GetStream();

                string message = "Привет, сервер!";
                byte[] data = Encoding.UTF8.GetBytes(message);
                stream.Write(data, 0, data.Length);
                Console.WriteLine("Сообщение отправлено");

                // Чтение ответа
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Ответ от сервера: {response}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}
