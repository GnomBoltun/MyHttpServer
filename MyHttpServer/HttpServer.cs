using System.Text;
using System.Net;

namespace MyHttpServer;

public class HttpServer
{
    private HttpListener? _listener;

            public HttpServer(string[] prefixes)
            {
                if (!HttpListener.IsSupported)
                {
                    throw new Exception("HttpListener is not supported");

                }

                _listener = new HttpListener();

                foreach (string prefix in prefixes) _listener.Prefixes.Add(prefix);

            }

            public async Task Start()
            {
                _listener?.Start();
                Console.WriteLine("http Server Started");


                do
                {

                    Console.WriteLine(DateTime.Now.ToLongTimeString() + "waiting a client connect");
                    var context = await _listener?.GetContextAsync();
                    await ProcessRequest(context);

                    Console.WriteLine(DateTime.Now.ToLongTimeString() + " client connected");
                }
                while (_listener.IsListening);
            }

            async Task ProcessRequest(HttpListenerContext context)
            {
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;

                Console.WriteLine($"{request.HttpMethod} {request.RawUrl} {request.Url?.AbsolutePath}");

                var outputStream = response.OutputStream;

                switch (request.Url?.AbsolutePath)
                {
                    //http://localhost:8888/dodo

                    case "/dodo":
                        {
                            // FileStream fstream = File.OpenRead(@"C:\Users\SURFACE\source\repos\HttpServer\HttpServer\google\google.html");
                            // byte[] buffer = new byte[fstream.Length];

                            var fileName = "index.html";
                            string dodo = File.ReadAllText(fileName);

                            byte[] buffer = Encoding.UTF8.GetBytes(dodo);
                            response.ContentLength64 = buffer.Length;
                            await outputStream.WriteAsync(buffer, 0, buffer.Length);
                        }
                        break;


                }

                outputStream.Close();

            }
}