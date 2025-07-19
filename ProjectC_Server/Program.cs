namespace ProjectC_Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            // Initialize the server

            Server server = new Server();

            while(true)
            {
                string input = Console.ReadLine();
                if (input?.ToLower() == "exit")
                {
                    Console.WriteLine("Exiting server...");
                    break;
                }
                else
                {
                    Console.WriteLine("Type 'exit' to stop the server.");
                }
            }
        }
    }
}
