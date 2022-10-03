// See https://aka.ms/new-console-template for more information
using static MyHttpServer.HttpServer;

namespace MyHttpServer
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var server = new HttpServer(new string[] { "http://localhost:8888/" });
            await server.Start();
        }
    }
}


