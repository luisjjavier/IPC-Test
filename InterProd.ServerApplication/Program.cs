using System;
using System.IO;
using System.IO.Pipes;
using System.Linq;

namespace InterProd.ServerApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            StartServer();
        }

        private static void StartServer()
        {
            var server = new NamedPipeServerStream("PipesOfPiece");
            server.WaitForConnection();
            ReadClient(server);
          
        }

        private static void ReadClient(NamedPipeServerStream server)
        {
            StreamReader reader = new StreamReader(server);
            StreamWriter writer = new StreamWriter(server);
            while (true)
            {
                var line = reader.ReadLine();
                if (string.IsNullOrEmpty(line))
                {
                    server.Close();
                    server = new NamedPipeServerStream("PipesOfPiece");
                    server.WaitForConnection();
                     reader = new StreamReader(server);
                     writer = new StreamWriter(server);
                    continue;
                }
                Console.WriteLine($"Recibiendo...{line}");
                writer.WriteLine(string.Join("", line.Reverse()));
                writer.Flush();
            }
        }
    }
}
