using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace _01_10_2022_server_http_steam
{
    internal class HttpServer
    {
        private readonly string _url;

        private readonly HttpListener _listener;

        public HttpServer(string port, string name)
        {
            _listener = new HttpListener();
            _url = port;
            _listener.Prefixes.Add("http://localhost:" + port + "/" + name + "/");
        }

        public void Start()
        {
            _listener.Start();
        }

        public void Stop()
        {
            _listener.Stop();
        }

        public void MakeResponse(string pathOfFile)
        {
            HttpListenerContext _httpContext = _listener.GetContext();

            HttpListenerRequest request = _httpContext.Request;

            HttpListenerResponse response = _httpContext.Response;

            string responseStr = File.ReadAllText(pathOfFile);
            byte[] buffer = Encoding.UTF8.GetBytes(responseStr);

            response.ContentLength64 = buffer.Length;
            Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);

            output.Close();
        }
    }
}
