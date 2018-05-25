using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Threading.Tasks;

namespace InterProd.ServerApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            BlockingCollection<string> queue =
                new BlockingCollection<string>();
            Task.Factory.StartNew(() => { StartServer(queue); });

            while (true)
            {
                if (queue.Count == 0)
                {
                    Console.WriteLine(1);
                }
                else
                {
                    Console.WriteLine("Me pongo a lo que tnego que hacer");
                    Console.ReadLine();
                    queue.Take();
                }
            }
        }

        private static void StartServer(BlockingCollection<string> queueCollection)
        {
            var server = new NamedPipeServerStream("PipesOfPiece");
            server.WaitForConnection();
            ReadClient(server, queueCollection);
        }

        private static void ReadClient(NamedPipeServerStream server, BlockingCollection<string> queueCollection)
        {
            StreamReader reader = new StreamReader(server);

            StreamWriter writer = new StreamWriter(server);
            var line = reader.ReadLine();
            queueCollection.Add(line);
            writer.WriteLine(line);
            Console.WriteLine($"Recibiendo...{line}");
              writer.WriteLine(string.Join("", line?.Reverse()));
            //writer.Flush();
            server.Close();
            StartServer( queueCollection);
            //while (true)
            //{
            //    var line = reader.ReadLine();
            //    if (string.IsNullOrEmpty(line))
            //    {
            //        server.Close();
            //        server = new NamedPipeServerStream("PipesOfPiece");
            //        server.WaitForConnection();
            //        reader = new StreamReader(server);
            //        writer = new StreamWriter(server);
            //        continue;
            //    }
            //    Console.WriteLine($"Recibiendo...{line}");
            //    writer.WriteLine(string.Join("", line.Reverse()));
            //    writer.Flush();
            //}
        }
    }
}