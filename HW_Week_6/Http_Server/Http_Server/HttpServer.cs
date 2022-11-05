using System.Net;
using System.Text;

namespace Http_Server;

public class HttpServer : IDisposable
{
    private readonly HttpListener _listener;
    private ServerSettings _serverSetting;
    public ServerStatus Status { get; private set; }
    

    public HttpServer()
    {
        _listener = new HttpListener();
        Status = ServerStatus.Stop;
        _serverSetting = new ServerSettings();
    }

    public void Start()
    {
        if (Status == ServerStatus.Start) return;
        _listener.Start();
        Status = ServerStatus.Start; 
        
        _serverSetting = ServerFileHandle.ReadJsonSettings("./settings.json"); 
        
        _listener.Prefixes.Clear();
        _listener.Prefixes.Add($"http://localhost:{_serverSetting.Port}/");
  
        Listening();    
    }

    private void Stop()
    {
        if (Status == ServerStatus.Stop)
        {
            return;
        }
        
        _listener.Stop();

        Status = ServerStatus.Stop;
    }

    private void Listening()
    {
        _listener.BeginGetContext(new AsyncCallback(ListenerCallBack), _listener);
    }

    private void  ListenerCallBack(IAsyncResult result)
    {
        if (!_listener.IsListening) return;
        try
        {
            var context = _listener.EndGetContext(result);
            var response = context.Response;
            
            var serverResponse = ServerResponseProvider.GetResponse(_serverSetting.Path, context.Request);
            
            response.Headers.Set("Content-Type", serverResponse.ContentType);
            response.StatusCode = (int)serverResponse.ResponseStatusCode;
            
            if (serverResponse.ResponseStatusCode is HttpStatusCode.Redirect)
                response.Redirect(@"http://steampowered.com");

            var output = response.OutputStream;

            output.WriteAsync(serverResponse.Buffer, 0, serverResponse.Buffer.Length);
            Task.WaitAll();
            
            output.Close();
            response.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine($"{e}");
        }

        Listening();
    }

    public void Dispose()
    {
        Stop();
    }
}