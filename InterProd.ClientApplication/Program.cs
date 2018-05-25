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

            writer.WriteLine(args[0]);
            writer.Flush();
            client.Close();
        }

        private static void StartClient()
        {
   

           
        }
    }
}
