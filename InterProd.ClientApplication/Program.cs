using System;
using System.IO;
using System.IO.Pipes;

namespace InterProd.ClientApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            StartClient();
            var client = new NamedPipeClientStream("PipesOfPiece");
            client.Connect();
            StreamReader reader = new StreamReader(client);
            StreamWriter writer = new StreamWriter(client);
            while (true)
            {
                string input = Console.ReadLine();
                if (String.IsNullOrEmpty(input)) break;
                writer.WriteLine(input);
                writer.Flush();
                Console.WriteLine(reader.ReadLine());
            }
            client.Close();
        }

        private static void StartClient()
        {
   

           
        }
    }
}
